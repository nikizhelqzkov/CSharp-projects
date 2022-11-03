using MyMicroservice.DataAccess.DataProvider.Interfaces;
using MyMicroservice.DataContext;
using MyMicroservice.Models;
using Microsoft.EntityFrameworkCore;
namespace MyMicroservice.DataAccess.DataProvider.Clients
{
    public sealed class AuthProvider : IAuthProvider
    {

        public AuthProvider(BikeStoresDBContext dbContext)
        {
            DbContext = dbContext;
        }
        public BikeStoresDBContext DbContext { get; }

        public Customer GetCustomerIfHasSameEmail(string email, string firstName, string lastname)
        {
            var user = (from customer in DbContext.Customers
                        where customer.Email == email && customer.FirstName == firstName && customer.LastName == lastname
                        select customer).FirstOrDefault();

            return user;
        }

        public User GetUser(string username)
        {
            return DbContext.Users.FirstOrDefault(x => x.Username == username);
        }

        public User GetUserById(int id)
        {
            var user = (from u in DbContext.Users
                        join c in DbContext.Customers on u.CustomerId equals c.CustomerId
                        where u.UserId == id
                        select new User
                        {
                            UserId = u.UserId,
                            Username = u.Username,
                            PasswordHash = u.PasswordHash,
                            PasswordSalt = u.PasswordSalt,
                            CustomerId = u.CustomerId,
                            Customer = new Customer
                            {
                                CustomerId = c.CustomerId,
                                FirstName = c.FirstName,
                                LastName = c.LastName,
                                Phone = c.Phone,
                                Email = c.Email,
                                Street = c.Street,
                                City = c.City,
                                State = c.State,
                                ZipCode = c.ZipCode,
                            }
                        }).FirstOrDefault();
            return user;
        }

        public async Task<bool> HasSameUser(string username)
        {
            var result = await DbContext.Users.Where(x => x.Username == username).CountAsync() > 0;
            return result;
        }

        public async Task Register(User user)
        {
            DbContext.Users.Add(user);
            await DbContext.SaveChangesAsync();
        }


    }

}
