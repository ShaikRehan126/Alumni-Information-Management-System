using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Options;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MyMVCMappingDEMO;
public class SmtpEmailSender : IEmailSender
{
    private readonly EmailSettings _settings;

    public SmtpEmailSender(IOptions<EmailSettings> options)
    {
        _settings = options.Value;
    }

    public async Task SendEmailAsync(string email, string subject, string htmlMessage)
    {
        using var client = new SmtpClient(_settings.Host, _settings.Port)
        {
            Credentials = new NetworkCredential(_settings.UserName, _settings.Password),
            EnableSsl = _settings.EnableSSL
        };

        var mail = new MailMessage
        {
            From = new MailAddress(_settings.SenderEmail, _settings.SenderName),
            Subject = subject,
            Body = htmlMessage,
            IsBodyHtml = true
        };
        mail.To.Add(email);

        await client.SendMailAsync(mail);
    }
}

public class EmailSettings
{
    public string Host { get; set; }
    public int Port { get; set; }
    public bool EnableSSL { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
    public string SenderEmail { get; set; }
    public string SenderName { get; set; }
}