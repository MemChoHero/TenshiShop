namespace TenshiShop.Application.Exceptions.Auth;

public class RegistrationValidationException(string message) : Exception
{
    public override string Message { get; } = message;
}