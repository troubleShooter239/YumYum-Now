using System.Security.Claims;
using MVCWebApp.Auth;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication("Cookie")
.AddCookie("Cookie", config =>
{
    config.LoginPath = "";
});

builder.Services.AddAuthorization(options => 
{
    options.AddPolicy(Roles.ADMIN, builder =>
    {
        builder.RequireRole(ClaimTypes.Role, Roles.ADMIN);
    });

    options.AddPolicy(Roles.USER, builder =>
    {
        builder.RequireRole(ClaimTypes.Role, Roles.USER);
    });
});

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
