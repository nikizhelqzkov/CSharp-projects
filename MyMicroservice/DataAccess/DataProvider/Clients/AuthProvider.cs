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

        public async Task<User> GetUser(string username)
        {
            return await DbContext.Users.FirstOrDefaultAsync(x => x.Username == username);
        }

        public async Task<User> GetUserById(int id)
        {
            var user = await (from u in DbContext.Users
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
                              }).FirstOrDefaultAsync();
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
