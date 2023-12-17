namespace MVCWebApp.Services.EncryptorService;

/// <summary>
/// Interface for AES encryption operations.
/// </summary>

public interface IAesEncryptor
{
    /// <summary>
    /// Encrypts the input string and returns the Base64-encoded ciphertext.
    /// </summary>
    /// <param name="value">The value to be encrypted.</param>
    /// <returns>The encrypted data.</returns>
    string EncryptString(string value);

    /// <summary>
    /// Verifies if the stored encrypted data matches the encrypted form of the entered data.    
    /// </summary>
    /// <param name="storedData">The stored encrypted data.</param>
    /// <param name="enteredData">The entered data for verification.</param>
    /// <returns>True if the data match, otherwise false.</returns>
    bool VerifyString(string storedData, string enteredData);
}
