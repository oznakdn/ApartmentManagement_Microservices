using Financial.gRPC.Services;
using Financial.Application;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationService(
    commandConnectionString: builder.Configuration.GetConnectionString("FinanicalCommandConnection")!,
    queryConnectionString: builder.Configuration.GetConnectionString("FinanicalQueryConnection")!);

builder.Services.AddGrpc();

var app = builder.Build();

app.MapGrpcService<GreeterService>();
app.MapGrpcService<ExpencesService>();


app.MapGet("/", () => "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");

app.Run();
