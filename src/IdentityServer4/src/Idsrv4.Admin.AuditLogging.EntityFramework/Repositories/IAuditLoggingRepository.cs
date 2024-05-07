﻿using System.Threading.Tasks;
using Idsrv4.Admin.AuditLogging.EntityFramework.Entities;
using Idsrv4.Admin.AuditLogging.EntityFramework.Helpers.Common;

namespace Idsrv4.Admin.AuditLogging.EntityFramework.Repositories
{
    public interface IAuditLoggingRepository<TAuditLog>
    where TAuditLog : AuditLog
    {
        Task SaveAsync(TAuditLog auditLog);

        Task<PagedList<TAuditLog>> GetAsync(int page = 1, int pageSize = 10);

        Task<PagedList<TAuditLog>> GetAsync(string subjectIdentifier, string subjectName, string category, int page = 1,
            int pageSize = 10);
    }
}