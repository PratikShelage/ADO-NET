using AngularAuthApi.Models.Dto;
using MailKit.Net.Smtp;
using MimeKit;

namespace AngularAuthApi.Utility
{
    public class EmailService:IEmail
    {
        private IConfiguration _configuration;
        public EmailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public void SendEmail(EmailDTO emailDTO)
        {
            var emailmessage = new MimeMessage();
            var from = _configuration["EmailSettings:From"];
            emailmessage.From.Add(new MailboxAddress("Pratik shelage", from));
            emailmessage.To.Add(new MailboxAddress(emailDTO.to, emailDTO.to));
            emailmessage.Subject = emailDTO.subject;
            emailmessage.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = string.Format(emailDTO.contact)
            };

            using(var client = new SmtpClient())
            {
                try
                {
                    client.Connect(_configuration["EmailSettings:SmtpServer"], 465, true);
                    client.Authenticate(_configuration["EmailSettings:From"], _configuration["EmailSettings:Password"]);
                    client.Send(emailmessage);
                }catch (Exception ex)
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }

        }
    }
}
