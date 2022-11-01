using MyMicroservice.DataAccess.Responses;
using MyMicroservice.DataAccess.Requests;
using MyMicroservice.DTOModels;
using MyMicroservice.Models;

namespace MyMicroservice.Services
{
    public interface IAuthService
    {
        public bool PasswordVerify(UserLoginRequest request, UserDTO dbUser);
        public Task<UserRegisterResponse> Register(UserRegisterRequest request);
        public Task<bool> HasUser(string username);
        public Task<UserDTO> GetUser(string username);
        public string GenerateToken(UserDTO user);
        public string GetIdFromUser();
        public Task<UserResponse> GetUserById(int id);
        public void SetToken(string token);
    }
}
