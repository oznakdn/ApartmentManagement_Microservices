using AspNetCoreHero.ToastNotification;
using Client.WebAdmin.ClientServices;
using Client.WebAdmin.Filters;
using Client.WebAdmin.Handlers;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddHttpClient<AccountService>();
builder.Services.AddScoped<AccountService>();


builder.Services.AddHttpClient<ManagerService>();
builder.Services.AddScoped<ManagerService>();

builder.Services.AddHttpContextAccessor();


builder.Services.AddAuthentication("AuthScheme")
    .AddCookie("AuthScheme", options =>
    {
        options.LoginPath = "/Account/Login";
        options.LogoutPath = "/Account/Logout";
        options.AccessDeniedPath = "/Account/AccessDenied";
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
