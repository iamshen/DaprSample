using Microsoft.EntityFrameworkCore;
using Idsrv4.Admin.EntityFramework.Entities;
using Idsrv4.Admin.EntityFramework.Interfaces;
using Idsrv4.Admin.EntityFramework.Shared.Constants;
using Idsrv4.Admin.EntityFramework.Shared.Extensions;

namespace Idsrv4.Admin.EntityFramework.Shared.DbContexts;

public class AdminLogDbContext(DbContextOptions<AdminLogDbContext> options) : DbContext(options), IAdminLogDbContext
{
    public DbSet<Log> Logs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        ConfigureLogContext(builder);

        builder.UseSnakeCaseNames();
    }

    private void ConfigureLogContext(ModelBuilder builder)
    {
        builder.Entity<Log>(log =>
        {
            log.ToTable(TableConsts.Logging, TableConsts.Schema);
            log.HasKey(x => x.Id);
            log.Property(x => x.Level).HasMaxLength(128);
        });
    }
}