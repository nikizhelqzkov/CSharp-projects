using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMicroservice.DTOModels;
using MyMicroservice.DataContext;
using MyMicroservice.DataAccess.Responses;
using MyMicroservice.DataAccess.Requests;
using MyMicroservice.Services;
using Microsoft.AspNetCore.Authorization;
using System.Net;
using System.IdentityModel.Tokens.Jwt;

namespace MyMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly IConfiguration _configuration;
        public AuthController(IAuthService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterResponse>> Register(UserRegisterRequest request)
        {
            var response = await _service.Register(request);
            if (response == null)
            {
                return BadRequest("This user has registered before");
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public ActionResult<string> Login(UserLoginRequest request)
        {
            var user = _service.GetUser(request.UserName);
            if (user == null)
            {
                return BadRequest("Invalid username");
            }
            bool isValidPassword = _service.PasswordVerify(request, user);
            if (!isValidPassword)
            {
                return BadRequest("Invalid password");
            }
            string token = _service.GenerateToken(user);
            _service.SetToken(token);
            return Ok(token);
        }
        // GET: api/Auth
        [HttpGet, Authorize]
        public ActionResult<UserResponse> GetUser()
        {

            var stringUserId = _service.GetIdFromUser();
            if (stringUserId == null)
            {
                return BadRequest("Invalid User Id");
            }
            int userId = int.Parse(stringUserId);
            var user = _service.GetUserById(userId);

            return Ok(user);
        }
    }
}
