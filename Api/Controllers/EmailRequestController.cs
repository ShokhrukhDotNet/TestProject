using Api.ViewResponse;
using Domain.Entities.Models;
using Microsoft.AspNetCore.Mvc;
using Service.EmailRequests;

namespace Api.Controllers;

[Route("api/emailrequests")]
[ApiController]
public class EmailRequestController(IEmailRequestService emailRequestService) : ControllerBase
{
    [HttpPost("send")]
    public async Task<ApiResponse> SendAsync([FromBody] EmailRequest request)
    {
        await emailRequestService.SendEmailsAsync(request);
        return ApiResponse.Success("Emails sent successfully.");
    }
}
