using Microsoft.Extensions.DependencyInjection;
using System;

namespace MailService.Core.Configuration
{
    class MailServiceBuilder : IMailServiceBuilder
    {
        public MailServiceBuilder(IServiceCollection services)
        {
            Services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public IServiceCollection Services { get; }
    }
}
