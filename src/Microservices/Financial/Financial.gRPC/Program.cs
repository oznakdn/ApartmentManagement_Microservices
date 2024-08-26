using Financial.gRPC.Services;
using Financial.Application;
using Shared.Authentication;
using Shared.Logging;
using Shared.ExceptionHandling;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationService(
    commandConnectionString: builder.Configuration.GetConnectionString("FinanicalCommandConnection")!,
    queryConnectionString: builder.Configuration.GetConnectionString("FinanicalQueryConnection")!);

builder.Services.AddJwtAuthentication(opt =>
{
    opt.Issuer = builder.Configuration["JwtOption:Issuer"]!;
    opt.Audience = builder.Configuration["JwtOption:Audience"]!;
    opt.SecretKey = builder.Configuration["JwtOption:SecretKey"]!;
});

builder.Services.AddAuthorization();

builder.Services.AddGrpc().AddJsonTranscoding();

builder.Services.AddSerilogService();
builder.Services.AddExceptonHandlerService();
builder.UseSerilog();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


app.MapGrpcService<ExpencesService>();


app.Run();

/* Migration command
 * Write Db
  dotnet ef migrations add InitialWritedDb --context WriteDbContext --startup-project C:\Users\W10\OneDrive\Desktop\ApartmentManagement_Microservices\src\Microservices\Financial\Financial.gRPC\Financial.gRPC.csproj

  dotnet ef database update --context WriteDbContext --startup-project C:\Users\W10\OneDrive\Desktop\ApartmentManagement_Microservices\src\Microservices\Financial\Financial.gRPC\Financial.gRPC.csproj
 
 * Read Db
  dotnet ef migrations add InitialReadDb --context ReadDbContext --startup-project C:\Users\W10\OneDrive\Desktop\ApartmentManagement_Microservices\src\Microservices\Financial\Financial.gRPC\Financial.gRPC.csproj

  dotnet ef database update --context ReadDbContext --startup-project C:\Users\W10\OneDrive\Desktop\ApartmentManagement_Microservices\src\Microservices\Financial\Financial.gRPC\Financial.gRPC.csproj
 
 */