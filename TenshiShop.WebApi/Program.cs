using TenshiShop.Core.DependencyInjection;
using TenshiShop.Core.Constants;
using TenshiShop.WebApi.Middlewares;
using TenshiShop.Application.Settings;
using TenshiShop.WebApi.Jwt;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabaseServices(builder.Configuration.GetConnectionString(AppSettingsConstants.ConnectionString)!);
builder.Services.AddInfrastructureServices();
builder.Services.AddValidators();

builder.Services.AddScoped<IJwtTokenEncoder, JwtTokenEncoderWithEmail>();

builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("AuthOptions"));
var authOptions = builder.Configuration.GetSection("AuthOptions").Get<AuthOptions>()!;

builder.Services.AddAuthServices(authOptions);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();
app.MapControllers();

app.Run();