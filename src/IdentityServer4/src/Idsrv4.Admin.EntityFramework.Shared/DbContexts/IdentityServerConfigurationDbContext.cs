using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Entities;
using IdentityServer4.EntityFramework.Options;
using Microsoft.EntityFrameworkCore;
using Idsrv4.Admin.EntityFramework.Interfaces;
using Idsrv4.Admin.EntityFramework.Shared.Constants;
using Idsrv4.Admin.EntityFramework.Shared.Extensions;

namespace Idsrv4.Admin.EntityFramework.Shared.DbContexts;

public class IdentityServerConfigurationDbContext(
    DbContextOptions<IdentityServerConfigurationDbContext> options, ConfigurationStoreOptions storeOptions)
    : ConfigurationDbContext<IdentityServerConfigurationDbContext>(options, storeOptions), IAdminConfigurationDbContext
{
    public DbSet<ApiResourceProperty> ApiResourceProperties { get; set; }

    public DbSet<IdentityResourceProperty> IdentityResourceProperties { get; set; }

    public DbSet<ApiResourceSecret> ApiSecrets { get; set; }

    public DbSet<ApiScopeClaim> ApiScopeClaims { get; set; }

    public DbSet<IdentityResourceClaim> IdentityClaims { get; set; }

    public DbSet<ApiResourceClaim> ApiResourceClaims { get; set; }

    public DbSet<ClientGrantType> ClientGrantTypes { get; set; }

    public DbSet<ClientScope> ClientScopes { get; set; }

    public DbSet<ClientSecret> ClientSecrets { get; set; }

    public DbSet<ClientPostLogoutRedirectUri> ClientPostLogoutRedirectUris { get; set; }

    public DbSet<ClientIdPRestriction> ClientIdPRestrictions { get; set; }

    public DbSet<ClientRedirectUri> ClientRedirectUris { get; set; }

    public DbSet<ClientClaim> ClientClaims { get; set; }

    public DbSet<ClientProperty> ClientProperties { get; set; }

    public DbSet<ApiScopeProperty> ApiScopeProperties { get; set; }

    public DbSet<ApiResourceScope> ApiResourceScopes { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        ConfigureIdentityContext(builder);

        builder.UseSnakeCaseNames();
    }

    private void ConfigureIdentityContext(ModelBuilder builder)
    {
        //IdentityResource
        builder.Entity<IdentityResource>().ToTable(TableConsts.IdentityResource, TableConsts.Schema);
        builder.Entity<IdentityResourceClaim>().ToTable(TableConsts.IdentityClaim, TableConsts.Schema);
        builder.Entity<IdentityResourceProperty>().ToTable(TableConsts.IdentityResourceProperty, TableConsts.Schema);

        // ApiResource
        builder.Entity<ApiResource>().ToTable(TableConsts.ApiResource, TableConsts.Schema);
        builder.Entity<ApiResourceSecret>().ToTable(TableConsts.ApiSecret, TableConsts.Schema);
        builder.Entity<ApiResourceScope>().ToTable(TableConsts.ApiResourceScope, TableConsts.Schema);
        builder.Entity<ApiResourceClaim>().ToTable(TableConsts.ApiResourceClaim, TableConsts.Schema);
        builder.Entity<ApiResourceProperty>().ToTable(TableConsts.ApiResourceProperty, TableConsts.Schema);

        // Client
        builder.Entity<ClientGrantType>().ToTable(TableConsts.ClientGrantType, TableConsts.Schema);
        builder.Entity<ClientScope>().ToTable(TableConsts.ClientScope, TableConsts.Schema);
        builder.Entity<ClientSecret>().ToTable(TableConsts.ClientSecret, TableConsts.Schema);
        builder.Entity<ClientPostLogoutRedirectUri>().ToTable(TableConsts.ClientPostLogoutRedirectUri, TableConsts.Schema);
        builder.Entity<ClientCorsOrigin>().ToTable(TableConsts.ClientCorsOrigin, TableConsts.Schema);
        builder.Entity<ClientIdPRestriction>().ToTable(TableConsts.ClientIdPRestriction, TableConsts.Schema);
        builder.Entity<ClientRedirectUri>().ToTable(TableConsts.ClientRedirectUri, TableConsts.Schema);
        builder.Entity<ClientClaim>().ToTable(TableConsts.ClientClaim, TableConsts.Schema);
        builder.Entity<ClientProperty>().ToTable(TableConsts.ClientProperty, TableConsts.Schema);
        builder.Entity<Client>().ToTable(TableConsts.Client, TableConsts.Schema);

        //Api
        builder.Entity<ApiScope>().ToTable(TableConsts.ApiScope, TableConsts.Schema);
        builder.Entity<ApiScopeClaim>().ToTable(TableConsts.ApiScopeClaim, TableConsts.Schema);
        builder.Entity<ApiScopeProperty>().ToTable(TableConsts.ApiScopeProperty, TableConsts.Schema);

    }
}