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

builder.Services.AddGrpc();

builder.Services.AddSerilogService();
builder.Services.AddExceptonHandlerService();
builder.UseSerilog();

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();


app.MapGrpcService<ExpencesService>();


app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");



app.Run();
