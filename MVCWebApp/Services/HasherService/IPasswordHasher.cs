namespace MVCWebApp.Services.HasherService;

/// <summary>
/// Interface for hashing and verifying passwords.
/// </summary>
public interface IPasswordHasher
{
    /// <summary>
    /// Hashes the input password.
    /// </summary>
    /// <param name="password">The password to be hashed.</param>
    /// <returns>The hashed password.</returns>
    string HashString(string password);

    /// <summary>
    /// Verifies if the entered password matches the stored hashed password.
    /// </summary>
    /// <param name="storedPassword">The stored hashed password.</param>
    /// <param name="enteredPassword">The entered password for verification.</param>
    /// <returns>True if the passwords match, otherwise false.</returns>
    bool VerifyString(string storedPassword, string enteredPassword);
}
