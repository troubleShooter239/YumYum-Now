using System.Security.Cryptography;

namespace MVCWebApp.Tools;

public class PasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 20;
    private const int Iterations = 10_000;

    public static string HashString(string password)
    {
        // Generate a random salt
        byte[] salt;
        using (var rng = RandomNumberGenerator.Create())
        {
            salt = new byte[SaltSize];
            rng.GetBytes(salt);
        }

        // Create an instance of Rfc2898DeriveBytes with the password, salt, hash algorithm, and iteration count
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 
            Iterations, HashAlgorithmName.SHA256))
        {
            // Get the hash of the password
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Combine the salt and hash into one array
            byte[] hashBytes = new byte[SaltSize + HashSize];
            Array.Copy(salt, 0, hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            // Convert the bytes to a string for storage in the database
            return Convert.ToBase64String(hashBytes);
        }
    }

    public static bool VerifyString(string savedHash, string passwordToCheck)
    {
        // Convert the string back to bytes
        byte[] hashBytes = Convert.FromBase64String(savedHash);

        // Extract the salt from the first SaltSize bytes
        byte[] salt = new byte[SaltSize];
        Array.Copy(hashBytes, 0, salt, 0, SaltSize);

        // Create an instance of Rfc2898DeriveBytes with the entered password, 
        // extracted salt, hash algorithm, and iteration count
        using (var pbkdf2 = new Rfc2898DeriveBytes(passwordToCheck, salt, 
            Iterations, HashAlgorithmName.SHA256))
        {
            // Get the hash of the entered password
            byte[] hash = pbkdf2.GetBytes(HashSize);

            // Compare the hashes
            for (int i = 0; i < HashSize; i++)
            {
                if (hashBytes[i + SaltSize] != hash[i])
                    return false;
            }

            return true;
        }
    }
}
