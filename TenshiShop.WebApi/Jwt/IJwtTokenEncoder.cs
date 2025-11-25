namespace TenshiShop.WebApi.Jwt;

public interface IJwtTokenEncoder
{
    public string Encode(string field, TimeSpan expire);
}