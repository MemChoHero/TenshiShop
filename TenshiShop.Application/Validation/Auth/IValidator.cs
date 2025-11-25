namespace TenshiShop.Application.Validation.Auth;

public interface IValidator<in TRequest>
{
    void ValidateOrThrow(TRequest request);
}