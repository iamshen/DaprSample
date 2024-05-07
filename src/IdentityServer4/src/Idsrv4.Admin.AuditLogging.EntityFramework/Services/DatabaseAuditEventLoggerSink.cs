using System;
using System.Threading.Tasks;
using Idsrv4.Admin.AuditLogging.EntityFramework.Entities;
using Idsrv4.Admin.AuditLogging.EntityFramework.Mapping;
using Idsrv4.Admin.AuditLogging.EntityFramework.Repositories;
using Idsrv4.Admin.AuditLogging.Events;
using Idsrv4.Admin.AuditLogging.Services;

namespace Idsrv4.Admin.AuditLogging.EntityFramework.Services
{
    public class DatabaseAuditEventLoggerSink<TAuditLog> : IAuditEventLoggerSink 
        where TAuditLog : AuditLog, new()
    {
        private readonly IAuditLoggingRepository<TAuditLog> _auditLoggingRepository;

        public DatabaseAuditEventLoggerSink(IAuditLoggingRepository<TAuditLog> auditLoggingRepository)
        {
            _auditLoggingRepository = auditLoggingRepository;
        }

        public virtual async Task PersistAsync(AuditEvent auditEvent)
        {
            if (auditEvent == null) throw new ArgumentNullException(nameof(auditEvent));

            var auditLog = auditEvent.MapToEntity<TAuditLog>();

            await _auditLoggingRepository.SaveAsync(auditLog);
        }
    }
}