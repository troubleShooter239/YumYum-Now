namespace MVCWebApp.Services.HasherService;

/// <summary>
/// Utility class for cryptographic operations.
/// </summary>
public static class CryptographyUtils
{
    /// <summary>
    /// Generates random bytes and fills the provided buffer.
    /// </summary>
    /// <param name="buffer">The buffer to be filled with random bytes.</param>
    public static void GenerateRandomBytes(byte[] buffer)
    {
        using (var rng = System.Security.Cryptography.RandomNumberGenerator.Create())
            rng.GetBytes(buffer);
    }
}