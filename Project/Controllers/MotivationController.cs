using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.Services;
using System.Security.Claims;


namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class MotivationController : ControllerBase
    {
        private readonly IAIService _aiService;
        private readonly IEmailService _emailService;
        private readonly ApplicationDbContext _context;

        public MotivationController(IAIService aiService, IEmailService emailService, ApplicationDbContext context)
        {
            _aiService = aiService;
            _emailService = emailService;
            _context = context;
        }
        [HttpPost("send")]
        public async Task<IActionResult> SendMotivationalEmail()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized("Invalid token.");

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return BadRequest("Invalid user ID in token.");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return NotFound("User not found.");

            if (!user.IsEmailVerified)
                return BadRequest("Please verify your email first.");

            int daysSinceRegistration = (DateTime.Now - user.creationDate).Days;

            var motivationalContent = await _aiService.GenerateMotivationalMessageAsync(
                user.Name,
                user.Weight,
                user.Height,
                user.Age,
                daysSinceRegistration
            );

            await _emailService.SendMotivationalEmailAsync(user.Email, user.Name, motivationalContent);

            return Ok("Motivational email has been sent successfully!");
        }

        [HttpGet("preview")]
        public async Task<IActionResult> PreviewMotivationalMessage()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized("Invalid token.");

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return BadRequest("Invalid user ID in token.");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return NotFound("User not found.");

            int daysSinceRegistration = (DateTime.Now - user.creationDate).Days;

            var motivationalContent = await _aiService.GenerateMotivationalMessageAsync(
                user.Name,
                user.Weight,
                user.Height,
                user.Age,
                daysSinceRegistration
            );

            return Ok(new { message = motivationalContent });
        }

    }
}
