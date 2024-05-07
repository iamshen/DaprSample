using Microsoft.Extensions.DependencyInjection;
using Idsrv4.Admin.AuditLogging.EntityFramework.Extensions;

namespace Idsrv4.Admin.AuditLogging.Extensions
{
    public class AuditLoggingBuilder : IAuditLoggingBuilder
    {
        public AuditLoggingBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public IServiceCollection Services { get; }
    }
}