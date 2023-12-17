namespace MVCWebApp.Services.HasherService;

/// <summary>
/// Interface for generating salts.
/// </summary>
public interface ISaltGenerator
{
    /// <summary>
    /// Generates a random salt.
    /// </summary>
    /// <returns>The generated salt.</returns>
    byte[] GenerateSalt();
}
