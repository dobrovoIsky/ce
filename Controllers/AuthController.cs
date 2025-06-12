using Microsoft.AspNetCore.Mvc;
using BjuApiServer.Models;
using BjuApiServer.Data;
using Microsoft.EntityFrameworkCore;

namespace BjuApiServer.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _db;

        public AuthController(AppDbContext db)
        {
            _db = db;
        }

        // POST: api/auth/register
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] User user)
        {
            if (await _db.Users.AnyAsync(u => u.Username == user.Username))
                return BadRequest("❌ Користувач з таким логіном вже існує");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(user.PasswordHash);
            await _db.Users.AddAsync(user);
            await _db.SaveChangesAsync();

            return Ok("✅ Реєстрація успішна");
        }

        // POST: api/auth/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] User loginUser)
        {
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Username == loginUser.Username);
            if (user == null)
                return Unauthorized("❌ Користувача не знайдено");

            if (!BCrypt.Net.BCrypt.Verify(loginUser.PasswordHash, user.PasswordHash))
                return Unauthorized("❌ Неправильний пароль");

            return Ok("✅ Вхід успішний");
        }
    }
}
