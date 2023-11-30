using System.Security.Cryptography;

namespace MVCWebApp.Tools.Hashers;

public class PasswordHasher : IHasher
{
    private readonly byte _saltSize;
    private readonly byte _hashSize;
    private readonly short _iterations;

    public PasswordHasher(IConfiguration configuration)
    {
        _saltSize = byte.Parse(configuration["AppSettings:SaltSize"]!);
        _hashSize = byte.Parse(configuration["AppSettings:HashSize"]!);
        _iterations = short.Parse(configuration["AppSettings:Iterations"]!);
    }

    public string HashString(string password)
    {
        // Generate a random salt
        byte[] salt;
        using (var rng = RandomNumberGenerator.Create())
        {
            salt = new byte[_saltSize];
            rng.GetBytes(salt);
        }

        // Create an instance of Rfc2898DeriveBytes with the password, salt, hash algorithm, and iteration count
        using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 
            _iterations, HashAlgorithmName.SHA256))
        {
            // Get the hash of the password
            byte[] hash = pbkdf2.GetBytes(_hashSize);

            // Combine the salt and hash into one array
            byte[] hashBytes = new byte[_saltSize + _hashSize];
            Array.Copy(salt, 0, hashBytes, 0, _saltSize);
            Array.Copy(hash, 0, hashBytes, _saltSize, _hashSize);

            // Convert the bytes to a string for storage in the database
            return Convert.ToBase64String(hashBytes);
        }
    }

    public bool VerifyString(string savedHash, string passwordToCheck)
    {
        // Convert the string back to bytes
        byte[] hashBytes = Convert.FromBase64String(savedHash);

        // Extract the salt from the first _saltSize bytes
        byte[] salt = new byte[_saltSize];
        Array.Copy(hashBytes, 0, salt, 0, _saltSize);

        // Create an instance of Rfc2898DeriveBytes with the entered password, 
        // extracted salt, hash algorithm, and iteration count
        using (var pbkdf2 = new Rfc2898DeriveBytes(passwordToCheck, salt, 
            _iterations, HashAlgorithmName.SHA256))
        {
            // Get the hash of the entered password
            byte[] hash = pbkdf2.GetBytes(_hashSize);

            // Compare the hashes
            for (int i = 0; i < _hashSize; i++)
            {
                if (hashBytes[i + _saltSize] != hash[i])
                    return false;
            }

            return true;
        }
    }
}
