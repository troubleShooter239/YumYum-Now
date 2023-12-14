using System.Security.Cryptography;

namespace MVCWebApp.Tools.Interfaces;

// Implementation of the IEncrypter interface using AES encryption algorithm.
public class AesEncrypter : IEncrypter
{
    private readonly byte[] _key;
    private readonly byte[] _iv;

    public AesEncrypter(IConfiguration configuration)
    {
        _key = Convert.FromBase64String(configuration["AppSettings:EncryptionKey"]!);
        _iv = Convert.FromBase64String(configuration["AppSettings:InitializationVector"]!);
    }

    public string DecryptString(string? encryptedValue)
    {
        if (string.IsNullOrEmpty(encryptedValue))
        {
            return "";
        }

        using var aesAlg = Aes.Create();
        aesAlg.Key = _key;
        aesAlg.IV = _iv;

        var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

        using var msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedValue));
        using var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
        using var srDecrypt = new StreamReader(csDecrypt);

        return srDecrypt.ReadToEnd();
    }

    public string EncryptString(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            return "";
        }

        using var aesAlg = Aes.Create();
        aesAlg.Key = _key;
        aesAlg.IV = _iv;

        var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

        using var msEncrypt = new MemoryStream();
        using var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
        using var swEncrypt = new StreamWriter(csEncrypt);

        swEncrypt.Write(value);

        return Convert.ToBase64String(msEncrypt.ToArray());
    }
}
