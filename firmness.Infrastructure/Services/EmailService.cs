using System.Net;
using System.Net.Mail;
using firmness.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace firmness.Infrastructure.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var email = _configuration["EmailSettings:Email"];
        var password = _configuration["EmailSettings:Password"];

        using var smtp = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(email, password),
            EnableSsl = true
        };

        var message = new MailMessage
        {
            From = new MailAddress(email),
            Subject = subject,
            Body = body,
            IsBodyHtml = true
        };
        message.To.Add(to);
        await smtp.SendMailAsync(message);
    }
}