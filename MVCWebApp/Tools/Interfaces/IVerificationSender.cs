namespace MVCWebApp.Tools.Interfaces;

public interface IVerificationSender
{
    Task SendAsync(string contactInfo, string subject, string message);
}
