using Domain.Entities.Models;

namespace Service.EmailRequests;

public interface IEmailRequestService
{
    Task SendEmailsAsync(EmailRequest request);
}
