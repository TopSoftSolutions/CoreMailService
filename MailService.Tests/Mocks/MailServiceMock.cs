using MailService.Core.Configuration;
using MailService.Core.Models;
using MailService.Core.Response;
using MailService.Core.Services;
using System;
using System.Threading.Tasks;
using Xunit;

namespace MailService.Tests.Mocks
{
    public class MailServiceMock : IMailService
    {
        [Fact]
        Task<MailSentResult> IMailService.SendAsync(MailServiceOptions options, string profileName, Mail mail)
        {
            throw new NotImplementedException();
        }
    }
}
