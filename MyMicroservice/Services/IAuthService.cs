using MyMicroservice.DataAccess.Responses;
using MyMicroservice.DataAccess.Requests;
using MyMicroservice.DTOModels;
using MyMicroservice.Models;

namespace MyMicroservice.Services
{
    public interface IAuthService
    {
        public Task<bool> PasswordVerify(UserLoginRequest request, UserDTO dbUser);
        public Task<UserRegisterResponse> Register(UserRegisterRequest request);
        public Task<bool> HasUser(string username);
        public Task<UserDTO> GetUser(string username);
        public string GenerateToken(UserDTO user);
    }
}
