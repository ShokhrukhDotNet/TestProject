using System.Net;
using System.Net.Mail;
using Domain.Entities.Models;
using Microsoft.Extensions.Options;
using Repository.Users;

namespace Service.EmailRequests;

public class EmailRequestService : IEmailRequestService
{
    private readonly EmailSettings emailSettings;
    private readonly IUserRepository userRepository;

    public EmailRequestService(
        IOptions<EmailSettings> options,
        IUserRepository userRepository)
    {
        emailSettings = options.Value;
        this.userRepository = userRepository;
    }

    public async Task SendEmailsAsync(EmailRequest request)
    {
        using var smtp = new SmtpClient(emailSettings.SmtpServer, emailSettings.Port)
        {
            Credentials = new NetworkCredential(emailSettings.SenderEmail, emailSettings.Password),
            EnableSsl = emailSettings.EnableSsl
        };

        var users = await userRepository.GetByIdsAsync(request.UserIds);

        if (!users.Any())
            throw new Exception("No users found for given IDs");

        var message = new MailMessage
        {
            From = new MailAddress(emailSettings.SenderEmail, emailSettings.SenderName),
            Subject = request.Title,
            Body = request.Description,
            IsBodyHtml = true
        };

        foreach (var user in users)
        {
            if (!string.IsNullOrWhiteSpace(user.Email))
                message.Bcc.Add(user.Email);
        }

        await smtp.SendMailAsync(message);
    }
}
