using MyMicroservice.Models;

namespace MyMicroservice.DataAccess.DataProvider.Interfaces
{
    public interface IAuthProvider
    {
        Task<bool> HasSameUser(string username);
        Task Register(User user);
        Task<User> GetUser(string username);
        Task<User> GetUserById(int id);
    }
}
