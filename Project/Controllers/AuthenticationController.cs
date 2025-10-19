using Project.Data;
using Project.DTO;
using Project.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Linq;



namespace Project.Controllers
{
    [Route("api/[controller")]
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

        }
    }
}
