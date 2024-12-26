using System;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Idsrv4.Admin.EntityFramework.Shared.Constants;
using Idsrv4.Admin.EntityFramework.Shared.Entities.Identity;
using Idsrv4.Admin.EntityFramework.Shared.Extensions;

namespace Idsrv4.Admin.EntityFramework.Shared.DbContexts;

public class AdminIdentityDbContext(DbContextOptions<AdminIdentityDbContext> options) :
    IdentityDbContext<
        UserIdentity, 
        UserIdentityRole,
        Guid,
        UserIdentityUserClaim,
        UserIdentityUserRole,
        UserIdentityUserLogin, 
        UserIdentityRoleClaim,
        UserIdentityUserToken>(options)
{
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        ConfigureIdentityContext(builder);

        builder.UseSnakeCaseNames();
    }

    private void ConfigureIdentityContext(ModelBuilder builder)
    {
        builder.Entity<UserIdentityRole>().ToTable(TableConsts.IdentityRoles, TableConsts.Schema);
        builder.Entity<UserIdentityRoleClaim>().ToTable(TableConsts.IdentityRoleClaims, TableConsts.Schema);
        builder.Entity<UserIdentityUserRole>().ToTable(TableConsts.IdentityUserRoles, TableConsts.Schema);

        builder.Entity<UserIdentity>(b =>
        {
            b.ToTable(TableConsts.IdentityUsers, TableConsts.Schema);
            b.Property(x => x.Authentication).HasColumnType("jsonb");
        });
        
        builder.Entity<UserIdentityUserLogin>().ToTable(TableConsts.IdentityUserLogins, TableConsts.Schema);
        builder.Entity<UserIdentityUserClaim>().ToTable(TableConsts.IdentityUserClaims, TableConsts.Schema);
        builder.Entity<UserIdentityUserToken>().ToTable(TableConsts.IdentityUserTokens, TableConsts.Schema);
    }
}