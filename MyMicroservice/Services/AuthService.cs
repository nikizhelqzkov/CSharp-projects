using AutoMapper;
using Microsoft.IdentityModel.Tokens;
using MyMicroservice.DataAccess.DataProvider.Interfaces;
using MyMicroservice.DataAccess.Requests;
using MyMicroservice.DataAccess.Responses;
using MyMicroservice.DTOModels;
using MyMicroservice.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Net;

namespace MyMicroservice.Services
{
    public class AuthService : IAuthService
    {
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IAuthProvider _authProvider;


        public AuthService(IAuthProvider authProvider, IMapper mapper, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _authProvider = authProvider;
            _mapper = mapper;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }


        public bool PasswordVerify(UserLoginRequest request, UserDTO dbUser)
        {
            bool isValidPassword = VerifyPasswordHash(request.Password, dbUser.PasswordHash, dbUser.PasswordSalt);
            return isValidPassword;
        }

        private bool VerifyPasswordHash(string requestedPassword, byte[] passwordHash, byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt))
            {
                var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(requestedPassword));
                return computedHash.SequenceEqual(passwordHash);
            }

        }

        public string GenerateToken(UserDTO user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.NameIdentifier, user.UserId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:Token").Value));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);



            var token = new JwtSecurityToken(
                claims: claims,
                expires: DateTime.Now.AddMinutes(1),
                signingCredentials: creds);

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return jwt;
        }

        public async Task<UserRegisterResponse> Register(UserRegisterRequest request)
        {
            var user = new User();
            bool hasSameUser = await _authProvider.HasSameUser(request.Username);
            if (hasSameUser)
            {
                return null;
            }
            GeneratePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            user.Username = request.Username;
            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;
            user.Customer = FullfillCustomerInfo(request);
            var customerResponse = _mapper.Map<CustomerDTO>(user.Customer);
            var userResponse = _mapper.Map<UserDTO>(user);
            await _authProvider.Register(user);

            return new UserRegisterResponse
            {
                User = userResponse,
                UserInfo = customerResponse
            };

        }
        private void GeneratePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private Customer FullfillCustomerInfo(UserRegisterRequest request)
        {
            var customer = new Customer()
            {
                FirstName = request.FirstName,
                LastName = request.LastName,
                Email = request.Email,
                Phone = request.Phone ?? null,
                Street = request.Street ?? null,
                City = request.City ?? null,
                State = request.State ?? null,
                ZipCode = request.ZipCode ?? null,
            };
            return customer;
        }

        public async Task<bool> HasUser(string username)
        {
            bool hasSameUser = await _authProvider.HasSameUser(username);
            return hasSameUser;
        }

        public async Task<UserDTO> GetUser(string username)
        {
            var user = await _authProvider.GetUser(username);
            return _mapper.Map<UserDTO>(user);
        }

        public string GetIdFromUser()
        {
            var result = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            return result;
        }

        public void SetToken(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.Now.AddMinutes(1)
            };
            _httpContextAccessor.HttpContext.Response.Cookies.Append("TokenUser", token, cookieOptions);

        }

        public async Task<UserResponse> GetUserById(int id)
        {
            var user = await _authProvider.GetUserById(id);
            if (user == null)
            {
                return null;
            }
            var userResponse = _mapper.Map<UserDTO>(user);
            var customerResponse = _mapper.Map<CustomerDTO>(user.Customer);
            var token = _httpContextAccessor.HttpContext.Request.Cookies["TokenUser"];
            if (token == null)
            {
                return new UserResponse
                {
                    User = userResponse,
                    UserInfo = customerResponse
                };
            }
            return new UserResponse
            {
                User = userResponse,
                UserInfo = customerResponse,
                Token = token
            };



        }
    }
}

