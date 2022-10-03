using Microsoft.AspNetCore.Mvc;
using MyMicroservice.Models;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MyMicroservice.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly User[] _users = new[]
        {
            new User { Username = "JDN", Name = "John Doe", Age = 42 },
            new User { Username = "JDNW", Name = "Jane Doe", Age = 39 },
            new User { Username = "SD", Name = "Sammy Doe", Age = 13 },
        };


        // GET: api/<UserController>
        [HttpGet]
        public List<User> GetAllUsers()
        {
            return this._users.ToList();
        }

        [HttpGet("{id}")]
        public User GetUser(int id)
        {
            if (id > this._users.Length - 1)
            {
                return new User
                {
                    Username = "Username",
                    Name = "User name",
                    Age = 0
                };
            }

            return this._users[id];
        }
    }

}
