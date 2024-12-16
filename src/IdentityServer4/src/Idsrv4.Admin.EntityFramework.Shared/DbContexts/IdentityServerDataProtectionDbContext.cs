using Idsrv4.Admin.EntityFramework.Shared.Constants;
using Idsrv4.Admin.EntityFramework.Shared.Extensions;
using Microsoft.AspNetCore.DataProtection.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Idsrv4.Admin.EntityFramework.Shared.DbContexts;

public class IdentityServerDataProtectionDbContext(DbContextOptions<IdentityServerDataProtectionDbContext> options)
    : DbContext(options), IDataProtectionKeyContext
{
    public DbSet<DataProtectionKey> DataProtectionKeys { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        ConfigureIdentityContext(builder);

        builder.UseSnakeCaseNames();
    }

    private void ConfigureIdentityContext(ModelBuilder builder)
    {
        builder.Entity<DataProtectionKey>().ToTable(TableConsts.DataProtectionKey, TableConsts.Schema);
    }
}