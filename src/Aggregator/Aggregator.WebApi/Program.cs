using Aggregator.WebApi.Services;
using Shared.Authentication;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddHttpClient<ApartmentService>("Apartment", conf =>
{
    conf.BaseAddress = new Uri("https://localhost:7000/api/apartment");
});
builder.Services.AddScoped<ApartmentService>();

builder.Services.AddHttpClient<FinancialService>("Financial", conf =>
{
    conf.BaseAddress = new Uri("https://localhost:7000/api/financial");
});
builder.Services.AddScoped<FinancialService>();


builder.Services.AddHttpClient<SiteService>("Site",conf =>
{
    conf.BaseAddress = new Uri("https://localhost:7000");
});

builder.Services.AddScoped<SiteService>();

builder.Services.AddJwtAuthentication(opt =>
{
    opt.Issuer = builder.Configuration["JwtOption:Issuer"]!;
    opt.Audience = builder.Configuration["JwtOption:Audience"]!;
    opt.SecretKey = builder.Configuration["JwtOption:SecretKey"]!;
});

var app = builder.Build();


app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
