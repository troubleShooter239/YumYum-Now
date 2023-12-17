using System.Security.Cryptography;
using MVCWebApp.Models.PasswordHasherSettings;

namespace MVCWebApp.Services.HasherService;

/// <summary>
/// Service for hashing passwords and generating salts.
/// </summary>
public class PasswordHasher : IPasswordHasher, ISaltGenerator
{
    private readonly int _saltSize;
    private readonly int _hashSize;
    private readonly int _iterations;

    /// <summary>
    /// Initializes a new instance of the PasswordHasher class.
    /// </summary>
    /// <param name="settings">Password hasher settings.</param>
    public PasswordHasher(IPasswordHasherSettings settings)
    {
        _saltSize = settings.SaltSize;
        _hashSize = settings.HashSize;
        _iterations = settings.Iterations;
    }

    /// <summary>
    /// Generates a random salt.
    /// </summary>
    /// <returns>The generated salt.</returns>
    public byte[] GenerateSalt()
    {
        // Generate a random salt
        byte[] salt = new byte[_saltSize];
        CryptographyUtils.GenerateRandomBytes(salt);
        return salt;
    }

    /// <summary>
    /// Hashes the input password using a salt.
    /// </summary>
    /// <param name="password">The password to be hashed.</param>
    /// <returns>The hashed password.</returns>
    public string HashString(string password)
    {
        byte[] salt = GenerateSalt();
        byte[] hash;

        // Using PBKDF2 with SHA256 for password hashing
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, _iterations, HashAlgorithmName.SHA256))
        {
            hash = pbkdf2.GetBytes(_hashSize);
        }

        // Combine salt and hash and convert to Base64 string
        return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
    }

    /// <summary>
    /// Verifies if the entered password matches the stored hashed password.
    /// </summary>
    /// <param name="storedPassword">The stored hashed password.</param>
    /// <param name="enteredPassword">The entered password for verification.</param>
    /// <returns>True if the passwords match, otherwise false.</returns>
    public bool VerifyString(string storedPassword, string enteredPassword)
    {
        // Split stored password into salt and hash parts
        var passwordParts = storedPassword.Split(':');
        var salt = Convert.FromBase64String(passwordParts[0]);
        var hash = Convert.FromBase64String(passwordParts[1]);

        // Using PBKDF2 with SHA256 for password verification
        using (var pbkdf2 = new Rfc2898DeriveBytes(enteredPassword, salt, _iterations, HashAlgorithmName.SHA256))
        {
            // Compare the derived hash with the stored hash
            return Enumerable.SequenceEqual(pbkdf2.GetBytes(_hashSize), hash);
        }
    }
}