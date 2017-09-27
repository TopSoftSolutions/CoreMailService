using MailService.Core.Configuration;
using MailService.Core.Models;
using MailService.Core.Response;
using System.Threading.Tasks;

namespace MailService.Core.Services
{
    public interface IMailService
    {
        Task<MailSentResult> SendAsync(MailServiceOptions options, string profileName, Mail mail);
    }
}
