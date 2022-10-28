using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyMicroservice.DTOModels;
using MyMicroservice.DataContext;
using MyMicroservice.DataAccess.Responses;
using MyMicroservice.DataAccess.Requests;
using MyMicroservice.Services;

namespace MyMicroservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;
        private readonly IConfiguration _configuration;

        public AuthController(IAuthService service, IConfiguration configuration)
        {
            _service = service;
            _configuration = configuration;
        }
        [HttpPost("register")]
        public async Task<ActionResult<UserRegisterResponse>> Register(UserRegisterRequest request)
        {
            var response = await _service.Register(request);
            if (response == null)
            {
                return BadRequest("This user has registered before");
            }
            return Ok(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<string>> Login(UserLoginRequest request)
        {
            var user = await _service.GetUser(request.UserName);
            if (user == null)
            {
                return BadRequest("Invalid username");
            }
            bool isValidPassword = await _service.PasswordVerify(request, user);
            if (!isValidPassword)
            {
                return BadRequest("Invalid password");
            }
            string token = _service.GenerateToken(user);
            return Ok(token);
        }
        //// GET: api/Auth
        //[HttpGet]
        //public async Task<ActionResult<IEnumerable<UserDTO>>> GetUserDTO()
        //{
        //    return await _context.UserDTO.ToListAsync();
        //}

        //// GET: api/Auth/5
        //[HttpGet("{id}")]
        //public async Task<ActionResult<UserDTO>> GetUserDTO(int id)
        //{
        //    var userDTO = await _context.UserDTO.FindAsync(id);

        //    if (userDTO == null)
        //    {
        //        return NotFound();
        //    }

        //    return userDTO;
        //}

        //// PUT: api/Auth/5
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPut("{id}")]
        //public async Task<IActionResult> PutUserDTO(int id, UserDTO userDTO)
        //{
        //    if (id != userDTO.Id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Entry(userDTO).State = EntityState.Modified;

        //    try
        //    {
        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!UserDTOExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return NoContent();
        //}

        //// POST: api/Auth
        //// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        //[HttpPost]
        //public async Task<ActionResult<UserDTO>> PostUserDTO(UserDTO userDTO)
        //{
        //    _context.UserDTO.Add(userDTO);
        //    await _context.SaveChangesAsync();

        //    return CreatedAtAction("GetUserDTO", new { id = userDTO.Id }, userDTO);
        //}

        //// DELETE: api/Auth/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> DeleteUserDTO(int id)
        //{
        //    var userDTO = await _context.UserDTO.FindAsync(id);
        //    if (userDTO == null)
        //    {
        //        return NotFound();
        //    }

        //    _context.UserDTO.Remove(userDTO);
        //    await _context.SaveChangesAsync();

        //    return NoContent();
        //}

        //private bool UserDTOExists(int id)
        //{
        //    return _context.UserDTO.Any(e => e.Id == id);
        //}
    }
}
