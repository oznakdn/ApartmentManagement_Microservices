using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Shared.Logging;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddOcelot();

builder.Configuration
    .AddJsonFile("ocelot.json", true, true);

builder.Services.AddSerilogService();
builder.UseSerilog();

var app = builder.Build();

app.UseOcelot().Wait();
app.Run();
