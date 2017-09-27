using MailService.Core.Models;
using MailService.Core.Response;
using MailService.Core.Services;
using System;
using System.Threading.Tasks;

namespace MailService.Tests.Mocks
{
    public class MailServiceMock : IMailService
    {
        Task<MailSentResult> IMailService.SendAsync(string profileName, Mail mail)
        {
            throw new NotImplementedException();
        }
    }
}
