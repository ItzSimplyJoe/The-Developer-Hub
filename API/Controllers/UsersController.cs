using System.Security.Cryptography;
using API.Data;
using API.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly AppDbContext _appDbContext;
        public UsersController(AppDbContext appDbContext) => _appDbContext = appDbContext;

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetAllUsers()
        {
            return _appDbContext.User;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserAsync(Guid id)
        {
            if (id == Guid.Empty || id == null)
                return BadRequest();

            var user = await _appDbContext.User.FirstOrDefaultAsync(user => user.Id == id);

            if (user == null)
                return NotFound();

            return user;
        }

        [HttpGet("Login/{email}/{password}")]
        public async Task<ActionResult<User>> LoginAsync(string? email, string? password)
        {
            if (email == null || password == null)
                return BadRequest();

            var user = await _appDbContext.User.FirstOrDefaultAsync(user =>
                user.Email == email && user.Password == HashPassword(password));

            if (user == null)
                return NotFound();

            return user;
        }

        [HttpPost]
        public async Task<ActionResult> CreateUserAsync(User? user)
        {
            if (user == null)
                return BadRequest();

            if (await _appDbContext.User.Where(email => email.Email == user.Email).AnyAsync().ConfigureAwait(false))
                return BadRequest("Email Already Exists");

            user.Password = HashPassword(user.Password);

            _appDbContext.User.Add(user);

            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult<User>> UpdateUserAsync(User? user)
        {
            if (user == null)
                return BadRequest();

            var userData = await _appDbContext.User.FirstOrDefaultAsync(userData => userData.Id == user.Id);

            if (userData == null)
                return NotFound();

            foreach (var property in typeof(User).GetProperties())
            {
                var value = typeof(User).GetProperty(property.Name)?.GetValue(user);
                property.SetValue(userData, value);
            }

            return Ok();
        }

        public string HashPassword(string password)
        {
            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            var builder = new StringBuilder();
            foreach (var b in bytes)
            {
                builder.Append(b.ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
