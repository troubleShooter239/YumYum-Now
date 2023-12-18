namespace MVCWebApp.Models.JWTSettings;

public class JwtSettings : IJwtSettings
{
    public int TokenValidityMinutes { get; set; }
    public string SigningKey { get; set; } = string.Empty;
}
