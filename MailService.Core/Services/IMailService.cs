using MailService.Core.Models;
using MailService.Core.Response;
using System.Threading.Tasks;

namespace MailService.Core.Services
{
    public interface IMailService
    {
        Task<MailSentResult> SendAsync(string credentialName, Mail mail);
    }
}
