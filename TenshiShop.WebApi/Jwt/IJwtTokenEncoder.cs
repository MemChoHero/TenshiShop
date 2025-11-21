namespace TenshiShop.WebApi.Jwt;

public interface IJwtTokenEncoder
{
    public string Encode(string email, string secret);
}