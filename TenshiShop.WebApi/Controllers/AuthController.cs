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
    
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterCommand command, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(command, cancellationToken);
        
        return Ok(result);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginQuery query, CancellationToken cancellationToken)
    {
        var result = await _mediator.Send(query, cancellationToken);
        if (!result.IsSuccess)
        {
            return Unauthorized(new { result.Error });
        }

        var refreshToken = _jwtTokenEncoder.Encode(result.Value.Email, TimeSpan.FromDays(30));
        SetRefreshTokenToCookie(refreshToken);
        
        return Ok(new
        {
            Token = _jwtTokenEncoder.Encode(result.Value.Email, TimeSpan.FromMinutes(15))
        });
    }

    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh()
    {
        var refreshToken = Request.Cookies["refreshToken"];
        if (string.IsNullOrWhiteSpace(refreshToken))
        {
            return Forbid();
        }

        var email = _jwtTokenEncoder.Decode(refreshToken!);

        var newRefreshToken = _jwtTokenEncoder.Encode(email, TimeSpan.FromDays(30));
        SetRefreshTokenToCookie(newRefreshToken);

        return Ok(new { Token = _jwtTokenEncoder.Encode(email, TimeSpan.FromMinutes(15)) });
    }

    [HttpGet("confirm")]
    public async Task<IActionResult> Confirm(string code)
    {
        var result = await _mediator.Send(new ActivateUserCommand { Code = code });

        return Ok(result);
    }

    private void SetRefreshTokenToCookie(string token)
    {
        HttpContext.Response.Cookies.Append("refreshToken", token, new CookieOptions()
        {
            HttpOnly = true,
            SameSite = SameSiteMode.Strict,
            Secure = true,
        });
    }
}