﻿namespace MailService.Core.Models
{
    public class MailServiceCredentials
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public bool SSL { get; set; }
    }
}
