using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Idsrv4.Admin.AuditLogging.EntityFramework.Entities;

namespace Idsrv4.Admin.AuditLogging.EntityFramework.DbContexts
{
    public interface IAuditLoggingDbContext<TAuditLog> where TAuditLog : AuditLog
    {
        DbSet<TAuditLog> AuditLog { get; set; }

        Task<int> SaveChangesAsync();
    }
}