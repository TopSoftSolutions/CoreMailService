using MailService.Core.Models;
using System.Collections.Generic;

namespace MailService.Core.Configuration
{
    public class MailServiceOptions
    {
        public Dictionary<string, Profile> Profiles { get; set; } = new Dictionary<string, Profile>();


        public void TryValidate()
        {

        }
    }
}
