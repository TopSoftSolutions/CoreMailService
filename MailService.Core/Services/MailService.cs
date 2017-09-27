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

        public async Task<MailSentResult> SendAsync(string credentialName, Mail mail)
        {

            var credential = _options.Credentials[credentialName];

            if (credential == null)
            {
                return await Task.FromResult(new MailSentResult
                {
                    Succeeded = false,
                    Error = "Credentials not found"
                });
            }

            SmtpClient client = new SmtpClient(credential.Host, credential.Port)
            {
                EnableSsl = credential.SSL,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(credential.UserName, credential.Password)
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(credential.UserName)
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
