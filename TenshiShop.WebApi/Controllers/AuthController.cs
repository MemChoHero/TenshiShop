using MediatR;
using Microsoft.AspNetCore.Mvc;
using TenshiShop.Application.ApiCommands.Auth;
using TenshiShop.Application.ApiQueries.Auth;
using TenshiShop.WebApi.Jwt;

namespace TenshiShop.WebApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IJwtTokenEncoder _jwtTokenEncoder;

    public AuthController(IMediator mediator, IJwtTokenEncoder jwtTokenEncoder)
    {
        _mediator = mediator;
        _jwtTokenEncoder = jwtTokenEncoder;
    }
    
    [HttpPost("/register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }

    [HttpPost("/login")]
    public async Task<IActionResult> Login(
        [FromBody] LoginQuery query,
        CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        if (!result.IsSuccess)
        {
            return Unauthorized(new { result.Error });
        }

        var refreshToken = _jwtTokenEncoder.Encode(result.Value.Email, TimeSpan.FromDays(30));
        HttpContext.Response.Cookies.Append("refreshToken", refreshToken, new CookieOptions()
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true,
        });
        
        return Ok(new
        {
            Token = _jwtTokenEncoder.Encode(result.Value.Email, TimeSpan.FromMinutes(15))
        });
    }
}