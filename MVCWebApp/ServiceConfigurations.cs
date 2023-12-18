using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;
using MVCWebApp.Models.AesEncryptorSettings;
using MVCWebApp.Models.DbSettings;
using MVCWebApp.Models.JWTSettings;
using MVCWebApp.Models.PasswordHasherSettings;
using MVCWebApp.Services.EncryptorService;
using MVCWebApp.Services.HasherService;
using MVCWebApp.Services.ProductService;
using MVCWebApp.Services.UserService;

namespace MVCWebApp;

public static class ServiceConfigurations
{
    public static void ConfigureServices(WebApplicationBuilder builder)
    {
        var configuration = builder.Configuration;
        var services = builder.Services;

        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
                    configuration.GetSection("JwtSettings:SigningKey").Value!)),
                ValidateIssuer = false,
                ValidateAudience = false,
                ClockSkew = TimeSpan.Zero
            };
        });

        // Configure and register services
        // PasswordHasher service
        services.Configure<PasswordHasherSettings>(configuration.GetSection(nameof(PasswordHasherSettings)))
        .AddSingleton<IPasswordHasherSettings>(sp =>
            sp.GetRequiredService<IOptions<PasswordHasherSettings>>().Value)
        .AddScoped<IPasswordHasher, PasswordHasher>()
        // AesEncryptor service
        .Configure<AesEncryptorSettings>(configuration.GetSection(nameof(AesEncryptorSettings)))
        .AddSingleton<IAesEncryptorSettings>(sp =>
            sp.GetRequiredService<IOptions<AesEncryptorSettings>>().Value)
        .AddScoped<IAesEncryptor, AesEncryptor>()
        // JWT settings
        .Configure<JwtSettings>(configuration.GetSection(nameof(JwtSettings)))
        .AddSingleton<IJwtSettings>(sp =>
            sp.GetRequiredService<IOptions<JwtSettings>>().Value)
        // DB settings
        .Configure<YumYumNowDbSettings>(configuration.GetSection(nameof(YumYumNowDbSettings)))
        .AddSingleton<IYumYumNowDbSettings>(sp => 
            sp.GetRequiredService<IOptions<YumYumNowDbSettings>>().Value)
        .AddSingleton<IMongoClient>(s =>
            new MongoClient(configuration.GetValue<string>("YumYumNowDbSettings:ConnectionString")))
        // User service
        .AddScoped<IUserService, UserService>()
        // Product service
        .AddScoped<IProductService, ProductService>();

        services.AddControllersWithViews();
    }
}
