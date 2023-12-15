using System.Security.Claims;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MVCWebApp.Auth;
using MVCWebApp.Models.DbSettings;
using MVCWebApp.Services;

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

builder.Services.Configure<YumYumNowDbSettings>(
    builder.Configuration.GetSection(nameof(YumYumNowDbSettings))
);

builder.Services.AddSingleton<IYumYumNowDbSettings>(sp => 
    sp.GetRequiredService<IOptions<YumYumNowDbSettings>>().Value);

builder.Services.AddSingleton<IMongoClient>(s =>
    new MongoClient(builder.Configuration.GetValue<string>("YumYumNowDbSettings:ConnectionString")));

builder.Services.AddScoped<IUserService, UserService>();

// builder.Services.AddScoped<IMongoCollection<User>>(provider =>
// {
//     var configuration = provider.GetRequiredService<IConfiguration>();
//     var dbContext = new MongoDbContext(configuration);
//     return dbContext.Users;
// });

// builder.Services.AddScoped<ILogger<RegisterController>, Logger<RegisterController>>();
//builder.Services.AddScoped<IConfiguration, Configuration>();

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
