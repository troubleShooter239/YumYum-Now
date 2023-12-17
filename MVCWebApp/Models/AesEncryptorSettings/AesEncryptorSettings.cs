namespace MVCWebApp.Models.AesEncryptorSettings;

public class AesEncryptorSettings : IAesEncryptorSettings
{
    public string EncryptionKey { get; set; } = string.Empty;
    public string InitializationVector { get; set; } = string.Empty;
}
