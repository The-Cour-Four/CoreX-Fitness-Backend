using Project.Data;
using Project.DTO;
using Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Linq;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        //injecting the datacontext so we can communicate with the database
        public AuthenticationController(ApplicationDbContext context, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
        }
        //public emailfinder(request)
        //{
        //    if reqeust.email == User.email{
        //        return BadRequest();
        //    else
        //        {
        //            return BadRequest(""):
        //        }
        //    }
        //}
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
                Email = request.email,
                Weight = request.weight,
                Height = request.height,
                Age = request.age
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Ok("The user has been created successfully. ");
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(y => y.Email == request.email);

            if (user is null || !BCrypt.Net.BCrypt.Verify(request.password, user.passwordHashed))
                return BadRequest("The user does not exist, or the password is incorrect. ");

            string token = CreateToken(user);

            return Ok(new { token = token });
        }

        [HttpPost("forgotPassword")]
        public async Task<IActionResult> forgotPassword(ForgotPasswordDTO request)
        {

            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.newPassword))
                return BadRequest("Email and new password are required.");

            var user = await _context.Users.FirstOrDefaultAsync(z => z.Email == request.Email);
            if (user is null) return BadRequest("This email does not exist.");

            user.passwordHashed = BCrypt.Net.BCrypt.HashPassword(request.newPassword);

            await _context.SaveChangesAsync();
            return Ok("Password has been changed succesfully. ");
        }

        private string CreateToken(User user)
        {
            //Statements about the user(id, name , etc...)
            /*
             *                  دي البيانات الي هتتشفر لما تتحول و تتبعت
             *                  على شكل 
             *                     JSON
             *                     هل التوكين بتتغير لما باجي في اليوم التاسع ولا لأ؟؟؟؟؟؟
             */
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name)
            };


            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                _configuration.GetSection("AppSettings:Token").Value!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDecriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(14),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDecriptor);

            return tokenHandler.WriteToken(token);

        }
    }
}



