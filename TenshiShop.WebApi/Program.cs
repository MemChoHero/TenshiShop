using TenshiShop.Core.DependencyInjection;
using TenshiShop.Core.Constants;
using TenshiShop.WebApi.Settings;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddDatabaseServices(builder.Configuration.GetConnectionString(AppSettingsConstants.ConnectionString)!);
builder.Services.AddInfrastructureServices();

builder.Services.Configure<AuthOptions>(builder.Configuration.GetSection("AuthOptions"));
var authOptions = builder.Configuration.GetSection("AuthOptions").Get<AuthOptions>()!;



var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();