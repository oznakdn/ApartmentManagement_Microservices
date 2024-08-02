using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Account.Application;
using Shared.Authentication;
using Shared.Caching;
using Shared.Logging;
using Shared.ExceptionHandling;
using Account.Api;
using HealthChecks.UI.Client;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddApplicationService(
    builder.Configuration,
    builder.Configuration.GetConnectionString("AccountCommandConnection")!,
    builder.Configuration.GetConnectionString("AccountQueryConnection")!);

builder.Services.AddJwtAuthentication(opt =>
{
    opt.Issuer = builder.Configuration["JwtOption:Issuer"]!;
    opt.Audience = builder.Configuration["JwtOption:Audience"]!;
    opt.SecretKey = builder.Configuration["JwtOption:SecretKey"]!;
});

builder.Services.AddCaching(opt =>
{
    opt.Host = builder.Configuration.GetSection("RedisOption:Host")!.Value!;
    opt.Port = Convert.ToInt32(builder.Configuration.GetSection("RedisOption:Port")!.Value!);
    opt.Password = builder.Configuration.GetSection("RedisOption:Password")!.Value!;
    opt.ClientName = builder.Configuration.GetSection("RedisOption:ClientName")!.Value!;
});

builder.Services.AddSerilogService();
builder.Services.AddExceptonHandlerService();
builder.Services.AddProblemDetails();
builder.Services.AddHealthChecks()
    .AddCheck<HealtCheck>("Custom");

builder.UseSerilog();

var app = builder.Build();

app.UseGlobalExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();



/* Migration command
 * Command Db
  dotnet ef migrations add InitialCommandDb --context CommandDbContext --startup-project C:\Users\W10\OneDrive\Desktop\ApartmentManagement_Microservices\src\Microservices\Account\Account.WebApi\Account.WebApi.csproj

  dotnet ef database update --context CommandDbContext --startup-project C:\Users\W10\OneDrive\Desktop\ApartmentManagement_Microservices\src\Microservices\Account\Account.WebApi\Account.WebApi.csproj
 
 * Query Db
  dotnet ef migrations add InitialQueryDb --context QueryDbContext --startup-project C:\Users\W10\OneDrive\Desktop\ApartmentManagement_Microservices\src\Microservices\Account\Account.WebApi\Account.WebApi.csproj

  dotnet ef database update --context QueryDbContext --startup-project C:\Users\W10\OneDrive\Desktop\ApartmentManagement_Microservices\src\Microservices\Account\Account.WebApi\Account.WebApi.csproj
 
 */