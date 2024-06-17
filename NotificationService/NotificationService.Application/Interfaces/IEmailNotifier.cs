namespace NotificationService.Application.Interfaces;

public interface IEmailNotifier
{
    Task SendEmailAsync(string to, string from, string subject, string body);
}