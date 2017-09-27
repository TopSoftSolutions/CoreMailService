using MailService.Core.Models;
using System.Collections.Generic;

namespace MailService.Core.Configuration
{
    public class MailServiceOptions
    {
        public Dictionary<string, MailServiceCredentials> Credentials { get; set; } = new Dictionary<string, MailServiceCredentials>();


        public void TryValidate()
        {

        }
    }
}
