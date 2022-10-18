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
            new User { Username = "JDN", Name = "John Doe", Age = 42 , Id = 1},
            new User { Username = "JDNW", Name = "Jane Doe", Age = 39, Id = 2 },
            new User { Username = "SD", Name = "Sammy Doe", Age = 13, Id = 3 },
        };


        // GET: api/<UserController>
        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAll()
        {
            return Ok(this._users);
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUser(int id)
        {
            if (id > this._users.Length - 1)
            {
                return NotFound(id);
            }
            var result = this._users.Where(r => r.Id == id).FirstOrDefault();
            return Ok(result);
        }
    }

}
