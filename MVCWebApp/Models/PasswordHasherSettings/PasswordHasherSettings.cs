namespace MVCWebApp.Models.PasswordHasherSettings;

public class PasswordHasherSettings : IPasswordHasherSettings
{
    public int SaltSize { get; set; }
    public int HashSize { get; set; }
    public int Iterations { get; set; }
}
