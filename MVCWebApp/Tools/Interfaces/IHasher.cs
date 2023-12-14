namespace MVCWebApp.Tools.Interfaces;

// Provides methods for hashing and verifying strings.
public interface IHasher
{
    // Hashes the given string value.
    string HashString(string value);
    // Verifies a string against a saved hash.
    bool VerifyString(string savedHash, string valueToCheck);
}
