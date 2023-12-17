namespace MVCWebApp.Models.PasswordHasherSettings;

public interface IPasswordHasherSettings
{
    int SaltSize { get; set; }
    int HashSize { get; set; }
    int Iterations { get; set; }
}
