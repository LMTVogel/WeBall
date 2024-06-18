using System.Net;
using System.Net.Mail;
using NotificationService.Application.Interfaces;
using Polly;
using Serilog;

namespace NotificationService.Application.Services;

public class SmtpEmailNotifier(string smtpServer, int smtpPort, string userName, string password)
    : IEmailNotifier
{
    public async Task SendEmailAsync(string to, string from, string subject, string body)
    {
        using var client = new SmtpClient(smtpServer, smtpPort);
        client.UseDefaultCredentials = false;
        client.Credentials = new NetworkCredential(userName, password);

        var mailMessage = new MailMessage();
        mailMessage.From = new MailAddress(from);
        mailMessage.To.Add(to);
        mailMessage.Body = body;
        mailMessage.Subject = subject;
        
        await Policy
            .Handle<Exception>()
            .WaitAndRetryAsync(3, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)))
            .ExecuteAsync(async () => { await client.SendMailAsync(mailMessage); });
    }
}