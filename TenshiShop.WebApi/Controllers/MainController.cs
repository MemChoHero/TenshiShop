using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TenshiShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MainController : ControllerBase
    {
        [HttpGet("home")]
        public IActionResult Home()
        {
            return Ok(new { Message = "Welcome!!!" });
        }
    }
}
