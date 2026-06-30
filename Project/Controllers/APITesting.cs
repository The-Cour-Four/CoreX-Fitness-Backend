using Microsoft.AspNetCore.Mvc;

namespace Project.Controllers
{
    public class APITesting : ControllerBase
    {
        [HttpPost("ApiTesting")]
        public IActionResult APItesting(string x)
        {
            if (x is null)
                return Ok("WORKED WITH NULL! ");
            else
                return Ok("WORKED WITHOUT NULL! ");
        }
    }
}
