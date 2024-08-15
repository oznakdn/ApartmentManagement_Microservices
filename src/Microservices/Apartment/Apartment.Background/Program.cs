using Apartment.Background;
using Apartment.Background.ConsumerServices;
using Apartment.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDbContextPool<QueryDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("ApartmentCommandConnection")!));
builder.Services.AddDbContextPool<CommandDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("ApartmentQueryConnection")!));

builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<DeleteManagerFromSiteConsumer>();

var host = builder.Build();
host.Run();
