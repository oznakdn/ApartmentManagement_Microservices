using Shared.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddReverseProxy()
                .LoadFromConfig(builder.Configuration.GetSection("ReverseProxy"));

builder.Services.AddSerilogService();
builder.UseSerilog();

var app = builder.Build();

app.MapReverseProxy();

app.Run();
