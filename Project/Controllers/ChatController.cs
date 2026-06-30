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
    public class ChatController : ControllerBase
    {
        private readonly IAIService _aiService;
        private readonly ApplicationDbContext _context;
        public ChatController(IAIService aiService, ApplicationDbContext context)
        {
            _aiService = aiService;
            _context = context;
        }
        [HttpPost("ask")]
        public async Task<IActionResult> AskCoach(ChatRequestDTO request)
        {
            if (string.IsNullOrEmpty(request.Message))
                return BadRequest("The message cannot be empty!!!!! ");

            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null) return BadRequest("Invalid token! ");

            if (!int.TryParse(userIdClaim.Value, out int userId))
                return BadRequest("Invalid User ID in the token. ");

            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == userId);
            if (user == null) return BadRequest("User not found. ");

            var response = await _aiService.GetFitnessChatResponseAsync(
                request.Message,
                user.Weight,
                user.Height,
                user.Age
                );

            return Ok(new ChatResponseDTO
            {
                Response = response,
                TimeStamp = DateTime.Now
            }
                );
            
        }
    }
}
