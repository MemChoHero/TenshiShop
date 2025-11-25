using System.Net.Mail;
using System.Text.RegularExpressions;
using TenshiShop.Application.ApiCommands.Auth;
using TenshiShop.Application.Exceptions.Auth;

namespace TenshiShop.Application.Validation.Auth;

public class RegisterRequestValidator : IValidator<RegisterCommand>
{
    public void ValidateOrThrow(RegisterCommand request)
    {

        if (request.Name.Length < 3 | request.Name.Length > 20)
        {
            throw new RegistrationValidationException("Name length must be between 3 and 20 characters");
        }
        
        try
        {
           _ = new MailAddress(request.Email);
        }
        catch (FormatException)
        {
            throw new RegistrationValidationException("Email is invalid");
        }

        if (!Regex.IsMatch(request.Name, "^[a-zA-Z0-9_-]+$"))
        {
            throw new RegistrationValidationException("Name contains invalid characters");
        }

        if (!Regex.IsMatch(request.Password,
                @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?])[a-zA-Z0-9!@#$%^&*()_+\-=\[\]{};':""\\|,.<>\/?]{8,}$"))
        {
            throw new RegistrationValidationException("Password contains invalid characters");
        }
    }
}