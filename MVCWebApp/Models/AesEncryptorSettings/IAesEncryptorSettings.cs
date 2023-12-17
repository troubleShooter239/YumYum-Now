
namespace MVCWebApp.Models.AesEncryptorSettings;

public interface IAesEncryptorSettings
{
    string EncryptionKey { get; set; }
    string InitializationVector { get; set; }
}
