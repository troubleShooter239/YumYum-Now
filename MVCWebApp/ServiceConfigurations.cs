using System.Security.Claims;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MVCWebApp.Auth;
using MVCWebApp.Models.AesEncryptorSettings;
using MVCWebApp.Models.DbSettings;
using MVCWebApp.Models.PasswordHasherSettings;
using MVCWebApp.Services.EncryptorService;
using MVCWebApp.Services.HasherService;
using MVCWebApp.Services.UserService;

namespace MVCWebApp;

public static class ServiceConfigurations
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        var services = builder.Services;
        var configuration = builder.Configuration;

        // Authentication setup
        services.AddAuthentication("Cookie")
        .AddCookie("Cookie", config =>
        {
            config.LoginPath = "";
        });

        // Authorization setup
        services.AddAuthorization(options => 
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

        // Configure and register services
        // PasswordHasher service
        services.Configure<PasswordHasherSettings>(
            configuration.GetSection(nameof(PasswordHasherSettings))
        );
        services.AddSingleton<IPasswordHasherSettings>(sp =>
            sp.GetRequiredService<IOptions<PasswordHasherSettings>>().Value);
        services.AddScoped<IPasswordHasher, PasswordHasher>();

        // AesEncryptor service
        services.Configure<AesEncryptorSettings>(
            configuration.GetSection(nameof(AesEncryptorSettings))
        );
        services.AddSingleton<IAesEncryptorSettings>(sp =>
            sp.GetRequiredService<IOptions<AesEncryptorSettings>>().Value);
        services.AddScoped<IAesEncryptor, AesEncryptor>();

        // User service
        services.Configure<YumYumNowDbSettings>(
            configuration.GetSection(nameof(YumYumNowDbSettings))
        );
        services.AddSingleton<IYumYumNowDbSettings>(sp => 
            sp.GetRequiredService<IOptions<YumYumNowDbSettings>>().Value);
        services.AddSingleton<IMongoClient>(s =>
            new MongoClient(configuration.GetValue<string>("YumYumNowDbSettings:ConnectionString")));
        services.AddScoped<IUserService, UserService>();

        services.AddControllersWithViews();
    }
}
