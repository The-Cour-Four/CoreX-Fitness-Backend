using Project.Data;
using Project.DTO;
using Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Linq;



namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        //injecting the datacontext so we can communicate with the database
        public AuthenticationController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(UserRegisterDTO request)
        {
            if (await _context.Users.AnyAsync(e => e.Email == request.email))
                return BadRequest("The user alrady exists. ");

            string passwordHashed = BCrypt.Net.BCrypt.HashPassword(request.password);

            var user = new User
            {
                Name = request.userName,
                passwordHashed = passwordHashed,
                Email = request.email
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("The user has been created successfully. ");
        }
    }
}



