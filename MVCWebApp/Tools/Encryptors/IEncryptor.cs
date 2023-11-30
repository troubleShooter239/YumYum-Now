namespace MVCWebApp.Tools.Encryptors;

public interface IEncryptor
{
    string EncryptString(string value);
    string DecryptString(string encryptedValue);
}
