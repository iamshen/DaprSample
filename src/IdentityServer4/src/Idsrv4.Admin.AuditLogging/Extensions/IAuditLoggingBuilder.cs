using Microsoft.Extensions.DependencyInjection;

namespace Idsrv4.Admin.AuditLogging.Extensions
{
    public interface IAuditLoggingBuilder
    {
        IServiceCollection Services { get; }
    }
}