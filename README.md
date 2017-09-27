# CoreMailService

```c#
//Usings

using MailService.Core.Configuration;
using MailService.Core.Models;
```

```c#
//ConfigureServices
  services.UseMailService(opts =>
            {
                opts.Credentials.Add("Default", new Credential
                {
                    UserName = "example@domain.com",
                    Password = "supersecurepassword",
                    Host = "127.0.0.1",
                    Port = 25,
                    SSL = false
                });
            });


```
You can use [Papercut]: https://papercut.codeplex.com/  for local

```c#
//Usage in controller

 public class HomeController : Controller
    {
        private readonly IMailService _ms;

        public HomeController(Core.Services.MailService ms)
        {           
            _ms = ms;
        }

        public async Task<IActionResult> Index()
        {            
            var reciever = "example2@domain.com";
            var mail = new Mail
            {
                Subject = "Subject",
                Body = "Hey",
                IsBodyHtml = false
            };

            mail.To.Add(reciever);

            var result = await _ms.SendAsync("Default", mail);

            return Content($"Mail Sent: {result.Succeeded}");
        }
    }
```
