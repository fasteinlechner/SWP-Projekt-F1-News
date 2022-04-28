using F1_News.Models.Settings;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using MimeKit;
using System.IO;
using System.Threading.Tasks;

namespace F1_News.Models.Services {
    public class MailService : IMailService {
        private readonly MailSettings mailSettings;
        public async Task SendEmailAsync(MailRequest mailRequest) {
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(mailRequest.ToEmail));
            email.Subject = mailRequest.Subject;
            var builder = new BodyBuilder();
            builder.HtmlBody = mailRequest.Body;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
        }

        public async Task SendWelcomeEmailAsync(PersonalizedMail request) {
            StreamReader reader = new StreamReader(Directory.GetCurrentDirectory()+ "\\Models\\Services\\WelcomeTemp\\welcomeTemp.html");
            string Mail = reader.ReadToEnd();
            reader.Close();
            Mail = Mail.Replace("[Firstname]", request.Firstname).Replace("[Email]", request.ToEmail).Replace("[Username]", request.Username);
            var email = new MimeMessage();
            email.Sender = MailboxAddress.Parse(mailSettings.Mail);
            email.To.Add(MailboxAddress.Parse(request.ToEmail));
            email.Subject = $"Welcome {request.Firstname}";
            var builder = new BodyBuilder();
            builder.HtmlBody = Mail;
            email.Body = builder.ToMessageBody();
            using var smtp = new SmtpClient();
            smtp.Connect(mailSettings.Host, mailSettings.Port, SecureSocketOptions.StartTls);
            smtp.Authenticate(mailSettings.Mail, mailSettings.Password);
            await smtp.SendAsync(email);
            smtp.Disconnect(true);
            

        }

        public MailService(IOptions<MailSettings> mailSetting) {
            mailSettings = mailSetting.Value; 
        }
    }
}
