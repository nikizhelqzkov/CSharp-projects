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
