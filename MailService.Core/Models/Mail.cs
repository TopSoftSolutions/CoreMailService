using System.Collections.Generic;

namespace MailService.Core.Models
{
    public class Mail
    {
        public ICollection<string> To { get; set; } = new HashSet<string>();
        public string Subject { get; set; }
        public string Body { get; set; }
        public bool IsBodyHtml { get; set; }
    }
}