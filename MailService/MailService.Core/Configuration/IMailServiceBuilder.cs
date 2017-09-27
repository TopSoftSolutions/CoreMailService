using Microsoft.Extensions.DependencyInjection;

namespace MailService.Core.Configuration
{
    public interface IMailServiceBuilder
    {
        IServiceCollection Services { get; }
    }
}
