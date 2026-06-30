using MailKit;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Data;
using Project.DTO;
using Project.Services;
using System.Security.Claims;



namespace Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class ExerciseController : ControllerBase
    {
        private readonly IAIService _aIService;

        private readonly ApplicationDbContext _context;

        public ExerciseController(IAIService aIService, ApplicationDbContext context)
        {
            _aIService = aIService;

            _context = context;
        }

        [HttpPost("recommendations")]
        public async Task<IActionResult> GetRecommendations(ExerciseRequestDTO request)
        {
            if (string.IsNullOrWhiteSpace(request.FitnessGoal))
                return BadRequest("Fitness goal is required.");

            if (string.IsNullOrWhiteSpace(request.FitnessLevel))
                return BadRequest("Fitness level is required.");

            var validGoals = new[] { "weight loss", "muscle gain", "endurance", "flexibility", "general fitness" };
            if (!validGoals.Contains(request.FitnessGoal.ToLower()))
                return BadRequest($"Invalid fitness goal. Valid options: {string.Join(", ", validGoals)}");

            var validLevels = new[] { "beginner", "intermediate", "advanced" };

            if (!validLevels.Contains(request.FitnessLevel.ToLower()))
                return BadRequest($"Invalid fitness level. Valid options: {string.Join(", ", validLevels)}");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return Unauthorized("Invalid token.");

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return BadRequest("Invalid user ID in token.");


            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return NotFound("User not found.");

            var recommendations = await _aIService.GetExerciseRecommendationsAsync(
                user.Weight,
                user.Height,
                user.Age,
                request.FitnessGoal.ToLower(),
                request.FitnessLevel.ToLower()
            );

            return Content(recommendations, "application/json");
        }
    }
}
