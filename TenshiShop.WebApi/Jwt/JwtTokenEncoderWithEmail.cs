using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using TenshiShop.Application.Settings;

namespace TenshiShop.WebApi.Jwt;

public class JwtTokenEncoderWithEmail : IJwtTokenEncoder
{
    private readonly IOptions<AuthOptions> _authOptions;

    public JwtTokenEncoderWithEmail(IOptions<AuthOptions> authOptions)
    {
        _authOptions = authOptions;
    }
    
    public string Encode(string field, TimeSpan expire)
    {
        var claims = new List<Claim> { new (ClaimTypes.Email, field) };

        var jwt = new JwtSecurityToken(
            issuer: _authOptions.Value.Issuer,
            audience: _authOptions.Value.Audience,
            claims: claims,
            notBefore: DateTime.UtcNow,
            expires: DateTime.UtcNow.Add(expire),
            signingCredentials: new SigningCredentials(
                _authOptions.Value.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256
            )
        );
        
        return new JwtSecurityTokenHandler().WriteToken(jwt);
    }
}