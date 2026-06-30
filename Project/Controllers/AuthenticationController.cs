using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using Project.Data;
using Project.DTO;
using Project.Models;
using Project.Services;
using System.ComponentModel.DataAnnotations;
using System.Data.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        private readonly IEmailService _emailService;
        public static HashSet<string> _tokenBlackList = new HashSet<string>();
        public AuthenticationController(ApplicationDbContext context, IConfiguration configuration, IEmailService emailservice)
        {
            _context = context;
            _configuration = configuration;
            _emailService = emailservice;
        }

        

        [HttpPost("register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register(UserRegisterDTO request)
        {
            if (await _context.Users.AnyAsync(e => e.Email == request.email))
                return BadRequest("The user alrady exists. ");

            string passwordHashed = BCrypt.Net.BCrypt.HashPassword(request.password);

            //token for email verification.
            string verificationToken = Guid.NewGuid().ToString();

            var user = new User
            {
                Name = request.userName,
                passwordHashed = passwordHashed,
                Email = request.email,
                Weight = request.weight,
                Height = request.height,
                Age = request.age,
                Gender = request.gender,
                IsEmailVerified = false,
                EmailVerificationToken = verificationToken,
                EmailVerificationTokenExpiry = DateTime.Now.AddHours(1) // Token valid for 1 hours
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            //sending the verification email.
            await _emailService.SendVerificationEmailAsync(user.Email, user.Name, verificationToken);
            return Ok("The user has been created successfully. ");
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(UserLoginDTO request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(y => y.Email == request.email);

            if (user == null) return BadRequest("The user does not exist. ");

            if (user.LockoutEnd.HasValue && user.LockoutEnd > DateTime.Now)
            {
                var RemainingTime = (user.LockoutEnd.Value - DateTime.Now).Minutes + 1;
                return BadRequest($"The account is locked. Try again in {RemainingTime} minutes. ");
            }

            if (!BCrypt.Net.BCrypt.Verify(request.password, user.passwordHashed))
            {
                user.FailedLoginAttempts++;
                user.LastFailedLoginAttempt = DateTime.Now;

                if (user.FailedLoginAttempts >= 5)
                {
                    //locks the account after 5 failed attempts
                    user.LockoutEnd = DateTime.Now.AddMinutes(15);
                    await _context.SaveChangesAsync();
                    return BadRequest("The account has been locked due to several failed Login attempts. Try again in 15 minutes. ");
                }
                await _context.SaveChangesAsync();
                return BadRequest($"The user does not exist, or the password is incorrect. \n " +
                    $"{5 - user.FailedLoginAttempts} attempts remaining. ");
            }
                

          

            if (!user.IsEmailVerified) return BadRequest("Please verifiy your email before logging in. ");

            string token = CreateToken(user);
            user.FailedLoginAttempts = 0;
            user.LockoutEnd = null;
            user.LastFailedLoginAttempt = null;
            await _context.SaveChangesAsync();

            return Ok(new { token = token });
        }

        [HttpPost("unlockAccount")]
        public async Task<IActionResult> UnlockAccount(string email)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == email);
            if (user == null) return BadRequest("This user does not exist. ");

            user.FailedLoginAttempts = 0;
            user.LockoutEnd = null;
            user.LastFailedLoginAttempt = null;
            await _context.SaveChangesAsync();

            return Ok("Account has been unlocked. ");
        }

        [HttpPost("Logout")]
        [Authorize]
        public IActionResult Logout()
        {
            //get the token from the Authorization header
            var token = Request.Headers["Authorization"].ToString().Replace("Bearer ", "");

            //add token to blacklist
            if (!string.IsNullOrEmpty(token))
            {
                _tokenBlackList.Add(token);
            }

            return Ok("You have been logged out successfully.");
        }


        [HttpGet("getUserInfo")]
        [Authorize]
        public async Task<IActionResult> GetUserInfo()
        {

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim is null || !int.TryParse(userIdClaim.Value, out var userId))
                return Unauthorized("Invalid token.");
            // fetching user from database
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user is null)
                return NotFound("user not found");

            return Ok(new
            {
                id = user.Id,
                name = user.Name,
                email = user.Email,
                weight = user.Weight,
                height = user.Height,
                age = user.Age,
                gender = user.Gender,
                creationDate = user.creationDate
            });
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


        [HttpPost("PasswordChecker")]
        public async Task<IActionResult> PasswordChecker(PasswordCherckerDTO request)
        {
            var user = await _context.Users.FirstOrDefaultAsync(M => M.Email == request.CurrentEmail);


            if (user is null || (!BCrypt.Net.BCrypt.Verify(request.PasswordChecker, user.passwordHashed)))
                return BadRequest("The user does not exist or the password is incorrect. ");

            else
                return Ok();
        }


        [HttpPost("updateUserInformation")]
        public async Task<IActionResult> updateUserInformation(UpdateUserInformationDTO request) 
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == request.CurrentEmail);

            if (user is null) return BadRequest("This email does not exist. ");

            user.Name = request.userName;
            user.Email = request.email;

            if (!(string.IsNullOrEmpty(request.password)))
            {
                user.passwordHashed = BCrypt.Net.BCrypt.HashPassword(request.password);
            }

            user.Weight = request.weight;
            user.Height = request.height;
            user.Age = request.age;
            user.Gender = request.gender;
            await _context.SaveChangesAsync();
            return Ok("User information has been updated successfully.");
        }

        [HttpGet("verifyEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> VerifyEmail([FromQuery] string email, [FromQuery] string token)
        {
            if(string.IsNullOrEmpty(email) || string.IsNullOrEmpty(token))
                return BadRequest("Email and token are required.");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);

            if (user is null) return BadRequest("The user not found");
            if (user.IsEmailVerified) return BadRequest("The user is already verified");
            if (user.EmailVerificationToken != token) return BadRequest("Invalid verification token.");
            if(user.EmailVerificationTokenExpiry< DateTime.Now)return BadRequest("The verification token has expired.");

            //marks the email as verified.
            user.IsEmailVerified = true;
            user.EmailVerificationToken = null;
            user.EmailVerificationTokenExpiry = null;

            await _context.SaveChangesAsync();
            //redirects to the login page with success message.
            return Redirect("https://the-cour-four.github.io/CoreX-Fitness-Front-End/index.html");
        }

        [HttpPost("resendVerificationEmail")]
        [AllowAnonymous]
        public async Task<IActionResult> ResendEmailVerification(string email)
        {
            if (string.IsNullOrEmpty(email)) return BadRequest("The email is required. ");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
            if (user is null) return BadRequest("This user does not exist. ");

            if (user.IsEmailVerified) return BadRequest("This email is already verified. ");

            user.EmailVerificationToken = Guid.NewGuid().ToString();
            user.EmailVerificationTokenExpiry = DateTime.Now.AddHours(1);

            await _context.SaveChangesAsync();
            //sends the new email verification.
            await _emailService.SendVerificationEmailAsync(user.Email, user.Name, user.EmailVerificationToken);

            return Ok("The new email verification has been sent, please check your inbox again. ");
        }

        private string CreateToken(User user)
        {
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