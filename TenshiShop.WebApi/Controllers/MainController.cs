using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;
using TenshiShop.Application.ApiQueries.Main;

namespace TenshiShop.WebApi.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MainController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("home")]
        public async Task<IActionResult> Home()
        {
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            var result = await _mediator.Send(new HomeAccessQuery() { Email = email! });
            if (!result.IsSuccess)
            {
                var errorResponse = new
                {
                    Error = result.Error.Message
                };

                return StatusCode((int)HttpStatusCode.Forbidden, errorResponse);
            }

            return Ok(new { Message = "Welcome!!!" });
        }
    }
}
