using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Idsrv4.Admin.AuditLogging.EntityFramework.DbContexts;
using Idsrv4.Admin.AuditLogging.EntityFramework.Entities;

namespace Idsrv4.Admin.EntityFramework.Shared.DbContexts;

public class AdminAuditLogDbContext : DbContext, IAuditLoggingDbContext<AuditLog>
{
    public AdminAuditLogDbContext(DbContextOptions<AdminAuditLogDbContext> dbContextOptions)
        : base(dbContextOptions)
    {
    }

    public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

    public DbSet<AuditLog> AuditLog { get; set; }
}