using Microsoft.Extensions.DependencyInjection;
using System;

namespace MailService.Core.Configuration
{
    public static class Extensions
    {

        public static IMailServiceBuilder UseMailService(this IServiceCollection services, Action<MailServiceOptions> setupAction = null)
        {
            var mailServiceOptions = new MailServiceOptions();

            setupAction?.Invoke(mailServiceOptions);

            mailServiceOptions.TryValidate();

            services.AddSingleton(typeof(MailServiceOptions), mailServiceOptions);

            var builder = new MailServiceBuilder(services);

            return builder;
        }
    }
}
