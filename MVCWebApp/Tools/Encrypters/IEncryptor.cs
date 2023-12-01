namespace MVCWebApp.Tools.Encrypters;

// IEncrypter interface for data encryption and decryption.
public interface IEncrypter
{
    // Encrypts a given string value.
    string EncryptString(string? value);
    // Decrypts a given encrypted string.
    string DecryptString(string? encryptedValue);
}
