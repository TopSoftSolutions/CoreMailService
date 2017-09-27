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

        public async Task<MailSentResult> SendAsync(string credentialsName, Mail mail)
        {

            var credentials = _options.Credentials[credentialsName];

            if (credentials == null)
            {
                return await Task.FromResult(new MailSentResult
                {
                    Succeeded = false,
                    Error = "Credentials not found"
                });
            }

            SmtpClient client = new SmtpClient(credentials.Host, credentials.Port)
            {
                EnableSsl = credentials.SSL,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(credentials.UserName, credentials.Password)
            };

            MailMessage mailMessage = new MailMessage
            {
                From = new MailAddress(credentials.UserName)
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
