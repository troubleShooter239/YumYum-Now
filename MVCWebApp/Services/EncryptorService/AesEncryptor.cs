using System.Security.Cryptography;
using MVCWebApp.Models.AesEncryptorSettings;

namespace MVCWebApp.Services.EncryptorService;

/// <summary>
/// Implementation of IAesEncryptor using AES encryption algorithm.
/// </summary>
public class AesEncryptor : IAesEncryptor
{
    private readonly Aes _aesAlg;

    /// <summary>
    /// Initialize the AES algorithm with pre-defined key and IV.
    /// </summary>
    /// <param name="settings">Aes encryptor settings.</param>
    public AesEncryptor(IAesEncryptorSettings settings)
    {
        _aesAlg = Aes.Create();
        _aesAlg.Key = Convert.FromBase64String(settings.EncryptionKey);
        _aesAlg.IV = Convert.FromBase64String(settings.InitializationVector);
    }

    /// <summary>
    /// Encrypt the input string using AES algorithm and return the Base64-encoded ciphertext.
    /// </summary>
    /// <param name="value">The value to be encrypted.</param>
    /// <returns>The encrypted data.</returns>
    public string EncryptString(string value)
    {
        using (var encryptor = _aesAlg.CreateEncryptor())
        using (var msEncrypt = new MemoryStream())
        {
            using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
            using (var swEncrypt = new StreamWriter(csEncrypt, System.Text.Encoding.UTF8))
            {
                swEncrypt.Write(value);
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }
    }

    /// <summary>
    /// Verify if the stored encrypted data matches the encrypted form of the entered data.
    /// </summary>
    /// <param name="storedData">The encrypted stored data.</param>
    /// <param name="enteredData">The entered data for verification.</param>
    /// <returns>True if the data match, otherwise false.</returns>
    public bool VerifyString(string storedData, string enteredData)
        => storedData == EncryptString(enteredData);
}
