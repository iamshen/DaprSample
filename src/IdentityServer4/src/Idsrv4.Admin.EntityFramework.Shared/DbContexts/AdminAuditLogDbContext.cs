using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Idsrv4.Admin.AuditLogging.EntityFramework.DbContexts;
using Idsrv4.Admin.AuditLogging.EntityFramework.Entities;
using Idsrv4.Admin.EntityFramework.Shared.Constants;
using Idsrv4.Admin.EntityFramework.Shared.Extensions;

namespace Idsrv4.Admin.EntityFramework.Shared.DbContexts;

public class AdminAuditLogDbContext(DbContextOptions<AdminAuditLogDbContext> dbContextOptions)
    : DbContext(dbContextOptions), IAuditLoggingDbContext<AuditLog>
{
    public Task<int> SaveChangesAsync() => base.SaveChangesAsync();

    public DbSet<AuditLog> AuditLog { get; set; }

    /// <summary>
    ///     Override this method to further configure the model that was discovered by convention from the entity types
    ///     exposed in <see cref="T:Microsoft.EntityFrameworkCore.DbSet`1" /> properties on your derived context. The resulting model may be cached
    ///     and re-used for subsequent instances of your derived context.
    /// </summary>
    /// <remarks>
    ///     <para>
    ///         If a model is explicitly set on the options for this context (via <see cref="M:Microsoft.EntityFrameworkCore.DbContextOptionsBuilder.UseModel(Microsoft.EntityFrameworkCore.Metadata.IModel)" />)
    ///         then this method will not be run. However, it will still run when creating a compiled model.
    ///     </para>
    ///     <para>
    ///         See <see href="https://aka.ms/efcore-docs-modeling">Modeling entity types and relationships</see> for more information and
    ///         examples.
    ///     </para>
    /// </remarks>
    /// <param name="builder">
    ///     The builder being used to construct the model for this context. Databases (and other extensions) typically
    ///     define extension methods on this object that allow you to configure aspects of the model that are specific
    ///     to a given database.
    /// </param>
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        ConfigureIdentityContext(builder);

        builder.UseSnakeCaseNames();
    }

    private void ConfigureIdentityContext(ModelBuilder builder)
    {
        builder.Entity<AuditLog>().ToTable(TableConsts.AuditLog, TableConsts.Schema);
    }
}