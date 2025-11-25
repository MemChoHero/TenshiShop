using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace TenshiShop.Application.Settings;

public class AuthOptions
{
    public string Issuer { get; init; } = string.Empty;
    public string Audience { get; init; } = string.Empty;
    public string SecurityKey { get; init; } = string.Empty;
    public int ExpireToken { get; init; }
    
    public SymmetricSecurityKey GetSymmetricSecurityKey()
    {
        return new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey));
    }

}