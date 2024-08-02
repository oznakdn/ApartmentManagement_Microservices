using Notification.Application;
using Shared.Caching;
using Shared.Authentication;
using Shared.Logging;
using Shared.ExceptionHandling;
using Notification.WebSocket.Hubs;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddSignalR();
builder.Services.AddApplication(builder.Configuration);
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
builder.UseSerilog();

var app = builder.Build();

app.UseGlobalExceptionHandler();
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.MapHub<AnnouncementHub>("/announcement");

app.Run();
