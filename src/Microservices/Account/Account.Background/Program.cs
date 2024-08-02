using Account.Background;
using Account.Background.ConsumerServices;
using Account.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);
builder.Services.AddDbContextPool<CommandDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("AccountCommandConnection")!));
builder.Services.AddHostedService<Worker>();
builder.Services.AddSingleton<CreatedSiteConsumer>();
builder.Services.AddSingleton<AssignedManagerToSiteConsumer>();
builder.Services.AddSingleton<AssignedResidentToUnitConsumer>();
builder.Services.AddSingleton<AssignedEmployeeToSiteConsumer>();

var host = builder.Build();
host.Run();