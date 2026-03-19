using Application.Interfaces;
using Domain.helper;
using MimeKit;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Application.Services.Impelementation
{
    public class EmailsService : IEmailsService
    {
        #region Fields
        private readonly EmailSettings _emailSettings;
        #endregion
        #region Construcotrs
        public EmailsService(EmailSettings emailSettings)
        {
            _emailSettings = emailSettings;

        }
        #endregion
        #region Handle Function
        public async Task<string> SendEmails(string email, string Message, string? reason)
        {
            try
            {
                using (var client = new SmtpClient())
                {


                    await client.ConnectAsync(_emailSettings.Host, _emailSettings.Port, true);
                    client.Authenticate(_emailSettings.FromEmail, _emailSettings.Password);
                    var bodybuilder = new BodyBuilder
                    {
                        HtmlBody = $"{Message}",
                        TextBody = "Welcome XChange",
                    };
                    var message = new MimeMessage
                    {
                        Body = bodybuilder.ToMessageBody()
                    };
                    message.From.Add(new MailboxAddress("XchangeDevOps", _emailSettings.FromEmail));
                    message.To.Add(new MailboxAddress("Testing", email));
                    message.Subject = reason == null ? "No Submitted" : reason;
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
                return "Success";
            }
            catch (Exception ex)
            {
                return "Failed";
            }

        }
        #endregion

    }
}
