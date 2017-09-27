using MailService.Core.Configuration;
using MailService.Core.Models;
using MailService.Core.Response;
using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MailService.Core.Services
{
    public class MailService : IMailService
    {
        public async Task<MailSentResult> SendAsync(MailServiceOptions options, string profileName, Mail mail)
        {
            var profile = options.Profiles[profileName];

            if (profile == null)
            {
                return await Task.FromResult(new MailSentResult
                {
                    Succeeded = false,
                    Error = "Profile name not found"
                });
            }

            SmtpClient client = new SmtpClient(profile.Host, profile.Port)
            {
                EnableSsl = profile.SSL,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(profile.UserName, profile.Password)
            };

            MailMessage mailMessage = new MailMessage();
            mailMessage.From = new MailAddress(profile.UserName);
            foreach (var reciever in mail.To)
            {
                mailMessage.To.Add(reciever);
            }

            mailMessage.IsBodyHtml = mail.IsBodyHtml;
            mailMessage.Body = mail.Body;
            mailMessage.Subject = mail.Subject;

            try
            {
                await client.SendMailAsync(mailMessage);

                return await Task.FromResult(new MailSentResult
                {
                    Succeeded = true
                });
            }
            catch (Exception ex)
            {
                return await Task.FromResult(new MailSentResult
                {
                    Succeeded = false,
                    Error = ex.Message
                });
            }
        }

    }
}
