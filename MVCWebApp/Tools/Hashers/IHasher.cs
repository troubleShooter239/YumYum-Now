namespace MVCWebApp.Tools.Hashers;

public interface IHasher
{
    string HashString(string value);
    bool VerifyString(string savedHash, string valueToCheck);
}
