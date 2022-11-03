using MyMicroservice.Models;

namespace MyMicroservice.DataAccess.DataProvider.Interfaces
{
    public interface IAuthProvider
    {
        Task<bool> HasSameUser(string username);
        Task Register(User user);
        User GetUser(string username);
        User GetUserById(int id);
        Customer GetCustomerIfHasSameEmail(string email, string firstName, string lastname);
    }
}
