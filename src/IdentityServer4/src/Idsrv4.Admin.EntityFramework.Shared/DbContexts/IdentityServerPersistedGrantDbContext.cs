using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Idsrv4.Admin.EntityFramework.Interfaces;
using IdentityServer4.EntityFramework.Entities;
using Idsrv4.Admin.EntityFramework.Shared.Constants;
using Idsrv4.Admin.EntityFramework.Shared.Extensions;

namespace Idsrv4.Admin.EntityFramework.Shared.DbContexts;

public class IdentityServerPersistedGrantDbContext(
    DbContextOptions<IdentityServerPersistedGrantDbContext> options,
    OperationalStoreOptions storeOptions)
    : PersistedGrantDbContext<IdentityServerPersistedGrantDbContext>(options, storeOptions),
        IAdminPersistedGrantDbContext
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        ConfigureIdentityContext(builder);

        builder.UseSnakeCaseNames();
    }

    private void ConfigureIdentityContext(ModelBuilder builder)
    {
        builder.Entity<PersistedGrant>().ToTable(TableConsts.PersistedGrant, TableConsts.Schema);
        builder.Entity<DeviceFlowCodes>().ToTable(TableConsts.DeviceFlowCodes, TableConsts.Schema);

    }
}