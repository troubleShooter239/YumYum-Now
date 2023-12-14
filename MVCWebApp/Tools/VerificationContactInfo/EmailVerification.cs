using System.Net.Mail;
using MVCWebApp.Tools.Interfaces;

namespace MVCWebApp.Tools.VerificationContactInfo;

public class EmailVerification : IEmailVerification
{
    public Task SendAsync(string email, string subject, string message)
    {
        var mail = "YumYumNow@hotmail.com";
        var pw = "PopokSkinPopu";

        var client = new SmtpClient("smtp-mail.outlook.com", 587)
        {
            EnableSsl = true,
            Credentials = new System.Net.NetworkCredential(mail, pw)
        };

        return client.SendMailAsync(new MailMessage(from: mail, 
                                                    to: email,
                                                    subject,
                                                    message));
    }
}
