using MyMicroservice.DTOModels;
using MyMicroservice.Models;

namespace MyMicroservice.DataAccess.Responses
{
    public class UserRegisterResponse
    {
        public UserDTO User { get; set; }
        public string? Token { get; set; }

        public CustomerDTO UserInfo { get; set; }
    }
}
