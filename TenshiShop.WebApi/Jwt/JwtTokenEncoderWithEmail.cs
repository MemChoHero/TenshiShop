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

    public string Decode(string token)
    {
        var handler = new JwtSecurityTokenHandler();
        if (!handler.CanReadToken(token))
        {
            throw new ArgumentException("Invalid token format");
        }

        var decodedToken = handler.ReadJwtToken(token);

        return decodedToken.Claims.First(c => c.Type == ClaimTypes.Email).Value;
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