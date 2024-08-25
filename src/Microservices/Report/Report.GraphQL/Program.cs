using Report.GraphQL.Models;
using Report.GraphQL.Schemes;
using Report.GraphQL.Services;
using Shared.Authentication;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddJwtAuthentication(opt =>
{
    opt.Issuer = builder.Configuration["JwtOption:Issuer"]!;
    opt.Audience = builder.Configuration["JwtOption:Audience"]!;
    opt.SecretKey = builder.Configuration["JwtOption:SecretKey"]!;
});

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

builder.Services.AddGraphQLServer()
                .AddQueryType<Query>();

              
builder.Services.AddHttpClient<AggreatorService>("Aggregator", conf=> conf.BaseAddress = new Uri("https://localhost:7060"));
builder.Services.AddScoped<AggreatorService>();


var app = builder.Build();

app.UseAuthentication();

app.UseAuthorization();

app.MapGraphQL();

app.Run();
