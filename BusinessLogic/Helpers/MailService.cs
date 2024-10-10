using BusinessLogic.Interfaces;
using MimeKit;
using MailKit.Security;
using MimeKit.Text;
using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;

namespace BusinessLogic.Helpers
{
    public class MailService : IMailService
    {
        private readonly IConfiguration _configuration;
        public MailService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task SendMailAsync(string subject, string body, string toSend, string?
       fromSend = null)
        {
            SmtpClient smtpClient=new SmtpClient();
            //string EMAIL= "shrolts@gmail.com";
            // string emailFrom = _configuration["MailData:EmailFrom"];
            //Обовязково треба створити наприклад в папці Helpers обєкт MailData
            MailData data = _configuration.GetSection("MailData").Get<MailData>()!;
            string EMAIL = data.EmailFrom;
            string PASSWORD = data.Password;
            string HOST = data.Host;
            int PORT = data.Port;
            // create email message
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(EMAIL));
            email.To.Add(MailboxAddress.Parse(toSend));
            email.Subject = subject;
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = $"<h1>Your order </ h1 >< p >{ body }</ p > "
            };
            /* або створення тексту (for body) листа
                var bodyBuilder = new BodyBuilder();
                bodyBuilder.HtmlBody = $"<h1>Your order</h1><p>{body}</p>";
                bodyBuilder.TextBody = "This is some plain text";
                email.Body = bodyBuilder.ToMessageBody();
            */
            //client.Send(message);
            // send email
            using var smtp = new SmtpClient();
                //smtp.Connect("smtp.gmail.com", 587, SecureSocketOptions.StartTls);
                smtp.Connect(HOST, PORT, SecureSocketOptions.StartTls);
                smtp.Authenticate(EMAIL, PASSWORD);
                smtp.Send(email);
                smtp.Disconnect(true);
        }
    }
}