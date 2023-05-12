using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SendGrid;
using SendGrid.Helpers.Mail;

namespace Cegeka.Auction.Infrastructure;

public class SendGridMailServices : IEmailSender
{
    private readonly ILogger<SendGridMailServices> _logger;
    private IConfiguration _configuration;

    public SendGridMailServices(IConfiguration configuration, ILogger<SendGridMailServices> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public async Task SendEmailAsync(string toEmail, string subject, string content)
    {
        var apiKey = _configuration["SendGridKey"];
        var client = new SendGridClient(apiKey);
        var from = new EmailAddress("teodora.stroe2210@stud.acs.upb.ro", "Front Desk");
        var to = new EmailAddress(toEmail);
        var msg = MailHelper.CreateSingleEmail(from, to, subject, content, content);
        var response = await client.SendEmailAsync(msg);

        _logger.LogInformation("Sent e-mail!");
    }
}
