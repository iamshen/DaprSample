using Microsoft.EntityFrameworkCore;
using Idsrv4.Admin.EntityFramework.Constants;
using Idsrv4.Admin.EntityFramework.Entities;
using Idsrv4.Admin.EntityFramework.Interfaces;

namespace Idsrv4.Admin.EntityFramework.Shared.DbContexts;

public class AdminLogDbContext : DbContext, IAdminLogDbContext
{
    public AdminLogDbContext(DbContextOptions<AdminLogDbContext> options)
        : base(options)
    {
    }

    public DbSet<Log> Logs { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        ConfigureLogContext(builder);
    }

    private void ConfigureLogContext(ModelBuilder builder)
    {
        builder.Entity<Log>(log =>
        {
            log.ToTable(TableConsts.Logging);
            log.HasKey(x => x.Id);
            log.Property(x => x.Level).HasMaxLength(128);
        });
    }
}