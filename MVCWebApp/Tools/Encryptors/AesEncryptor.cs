using System.Security.Cryptography;
using System.Text;

namespace MVCWebApp.Tools.Encryptors;

public class AesEncryptor : IEncryptor
{
    private readonly string _key;
    private readonly string _iv;

    public AesEncryptor(IConfiguration configuration)
    {
        _key = configuration["AppSettings:EncryptionKey"]!;
        _iv = configuration["AppSettings:InitializationVector"]!;
    }

    public string DecryptString(string encryptedValue)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(_key);
            aesAlg.IV = Encoding.UTF8.GetBytes(_iv);

            var decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

            using (var msDecrypt = new MemoryStream(Convert.FromBase64String(encryptedValue)))
            {
                using (var csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
                {
                    using (var srDecrypt = new StreamReader(csDecrypt))
                    {
                        return srDecrypt.ReadToEnd();
                    }
                }
            }
        }
    }

    public string EncryptString(string value)
    {
        using (Aes aesAlg = Aes.Create())
        {
            aesAlg.Key = Encoding.UTF8.GetBytes(_key);
            aesAlg.IV = Encoding.UTF8.GetBytes(_iv);

            var encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

            using (var msEncrypt = new MemoryStream())
            {
                using (var csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (var swEncrypt = new StreamWriter(csEncrypt))
                    {
                        swEncrypt.Write(value);
                    }
                }
                return Convert.ToBase64String(msEncrypt.ToArray());
            }
        }
    }
}
