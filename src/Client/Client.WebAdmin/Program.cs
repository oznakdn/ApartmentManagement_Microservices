using AspNetCoreHero.ToastNotification;
using Client.WebAdmin.Filters;
using Client.WebAdmin.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddHttpClient();

builder.Services.AddHttpContextAccessor();

builder.Services.AddAuthentication("AuthScheme")
    .AddCookie("AuthScheme", options =>
    {
        options.LoginPath = "/Auth/Login";
        options.LogoutPath = "/Auth/Logout";
        options.AccessDeniedPath = "/Auth/AccessDenied";
    });


builder.Services.AddNotyf(conf =>
{
    conf.Position = NotyfPosition.TopRight;
    conf.DurationInSeconds = 3;
    conf.IsDismissable = true;
    conf.HasRippleEffect = true;
});

builder.Services.AddTransient<RefrehTokenHandler>();
builder.Services.AddTransient<AuthorizationHandler>();
builder.Services.AddScoped<TokenCheckFilter>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
