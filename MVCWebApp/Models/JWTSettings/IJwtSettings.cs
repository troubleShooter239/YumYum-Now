namespace MVCWebApp.Models.JWTSettings;

public interface IJwtSettings
{
    int TokenValidityMinutes { get; set; }
    string SigningKey { get; set; }
}
