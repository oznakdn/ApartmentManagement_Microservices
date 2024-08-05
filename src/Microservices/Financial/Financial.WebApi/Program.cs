using Financial.Application;
using Financial.WebApi;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Shared.Authentication;
using Shared.Caching;
using Shared.ExceptionHandling;
using Shared.Logging;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();

builder.Services.AddApplicationService(
    commandConnectionString:builder.Configuration.GetConnectionString("FinanicalCommandConnection")!,
    queryConnectionString: builder.Configuration.GetConnectionString("FinanicalQueryConnection")!);


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



/* Migration commands
 * Command Db
 dotnet ef migrations add Initial_Command_db --context CommandDbContext --startup-project C:\Users\W10\OneDrive\Desktop\ApartmentManagement_Microservices\src\Microservices\Financial\Financial.WebApi\Financial.WebApi.csproj

 dotnet ef database update --context CommandDbContext --startup-project C:\Users\W10\OneDrive\Desktop\ApartmentManagement_Microservices\src\Microservices\Financial\Financial.WebApi\Financial.WebApi.csproj

 * Query Db
 
dotnet ef migrations add Initial_Query_Db --context QueryDbContext --startup-project C:\Users\W10\OneDrive\Desktop\ApartmentManagement_Microservices\src\Microservices\Financial\Financial.WebApi\Financial.WebApi.csproj
 
 
dotnet ef database update --context QueryDbContext --startup-project C:\Users\W10\OneDrive\Desktop\ApartmentManagement_Microservices\src\Microservices\Financial\Financial.WebApi\Financial.WebApi.csproj

 */