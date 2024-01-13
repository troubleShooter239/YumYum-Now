using MVCWebApp.Models.UserDB;

namespace MVCWebApp.Services.JWTService;

/// <summary>
/// Interface for creating, validating jwt and sign in by it.
/// </summary>
public interface IJwtService
{
    /// <summary>
    /// Create token by input user.
    /// </summary>
    /// <param name="user">The user instance.</param>
    string Create(User user);
    /// <summary>
    /// SignIn by input jwt.
    /// </summary>
    /// <param name="jwt">The jwt string.</param>
    void SignIn(string jwt);

    /// <summary>
    /// Validating the input token.
    /// </summary>
    /// <param name="token">The token to be validating.</param>
    /// <returns>True if the token match, otherwise false.</returns>
    bool Validate(string token);
}
