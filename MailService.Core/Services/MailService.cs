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
        private MailServiceOptions _options = null;
        public MailService(MailServiceOptions options)
        {
            _options = options;
        }

        public async Task<MailSentResult> SendAsync(string profileName, Mail mail)
        {

            var profile = _options.Profiles[profileName];

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

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(profile.UserName)
            };
            foreach (var reciever in mail.To)
            {
                mailMessage.To.Add(reciever);
            }

            mailMessage.IsBodyHtml = mail.IsBodyHtml;
            mailMessage.Body = mail.Body;
            mailMessage.Subject = mail.Subject;
            MailSentResult mr = null;

            try
            {
                await client.SendMailAsync(mailMessage);
                mr = new MailSentResult
                {
                    Succeeded = true
                };

                return await Task.FromResult(mr);
            }
            catch (Exception ex)
            {
                mr = new MailSentResult
                {
                    Succeeded = false,
                    Error = "Mail sent failed"
                };

                return await Task.FromResult(mr);
            }
        }
    }
}
