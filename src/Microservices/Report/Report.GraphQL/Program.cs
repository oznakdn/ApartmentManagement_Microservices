using Report.GraphQL.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<AggreatorService>("Aggrigator",conf=> conf.BaseAddress = new Uri("https://localhost:7060/api/aggregator"));


var app = builder.Build();



app.Run();
