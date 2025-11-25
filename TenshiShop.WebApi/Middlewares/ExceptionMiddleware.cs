using TenshiShop.Application.Exceptions.Auth;

namespace TenshiShop.WebApi.Middlewares;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }
    
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (RegistrationValidationException e)
        {
            _logger.LogError(e.Message);
            
            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
            
            await httpContext.Response.WriteAsJsonAsync(
                new
                {
                    StatusCode = StatusCodes.Status400BadRequest,
                    e.Message
                });
        }
    }
}