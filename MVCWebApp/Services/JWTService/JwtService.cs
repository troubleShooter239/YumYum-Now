using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using MVCWebApp.Models.JWTSettings;
using MVCWebApp.Models.UserDB;

namespace MVCWebApp.Services.JWTService;

/// <summary>
/// Service for handling JWT (JSON Web Token) operations.
/// </summary>
public class JwtService : IJwtService
{
    private readonly IJwtSettings _jwtSettings;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <summary>
    /// Initializes a new instance of the <see cref="JwtService"/> class.
    /// </summary>
    /// <param name="jwtSettings">JWT settings injected through dependency injection.</param>
    /// <param name="httpContextAccessor">Accessor for accessing the current HTTP context.</param>
    public JwtService(IJwtSettings jwtSettings, IHttpContextAccessor httpContextAccessor) 
    {
        _jwtSettings = jwtSettings;
        _httpContextAccessor = httpContextAccessor;
    }

    /// <summary>
    /// Creates a JWT token for the provided user.
    /// </summary>
    /// <param name="user">User for whom the token is created.</param>
    /// <returns>Generated JWT token.</returns>
    public string Create(User user)
    {
        List<Claim> claims = new()
        {
            new Claim(ClaimTypes.Name, user.FirstName),
            new Claim(ClaimTypes.Surname, user.LastName),
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.MobilePhone, user.PhoneNumber),
            new Claim(ClaimTypes.StreetAddress, user.DeliveryAddress)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(
            _jwtSettings.SigningKey));

        return new JwtSecurityTokenHandler()
        .WriteToken(new JwtSecurityToken(
            claims: claims,
            expires: DateTime.Now.AddMinutes(_jwtSettings.TokenValidityMinutes),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature)
        ));
    }

    /// <summary>
    /// Signs in the user by appending the JWT token to the response cookies.
    /// </summary>
    /// <param name="jwt">JWT token to be stored in the cookie.</param>
    public void SignIn(string jwt)
    {
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTimeOffset.UtcNow.AddMinutes(_jwtSettings.TokenValidityMinutes),
            SameSite = SameSiteMode.Strict
        };

        _httpContextAccessor.HttpContext!.Response.Cookies.Append("JwtCookie", jwt, cookieOptions);
    }

    /// <summary>
    /// Validates the provided JWT token.
    /// </summary>
    /// <param name="token">JWT token to be validated.</param>
    /// <returns>True if the token is valid; otherwise, false.</returns>
    public bool Validate(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(_jwtSettings.SigningKey);

        try
        {
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            }, out SecurityToken validatedToken);

            return true;
        }
        catch (SecurityTokenException)
        {
            return false;
        }
    }
}
