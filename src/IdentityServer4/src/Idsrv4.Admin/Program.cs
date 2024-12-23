using Idsrv4.Admin.Shared.ModuleInitializer;
using Idsrv4.Admin.UI.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Idsrv4.Admin.UI.Helpers;

const string seedArgs = "/seed";
const string migrateOnlyArgs = "/migrateonly";

var builder = WebApplication.CreateBuilder(args.Where(x => x != seedArgs).ToArray());

builder.AddServiceDefaults();

#region Config

builder.Configuration.AddJsonFile("serilog.json", true, true);
builder.Configuration.AddJsonFile("identitydata.json", true, true);
builder.Configuration.AddJsonFile("identityserverdata.json", true, true);
if (builder.Environment.IsDevelopment()) builder.Configuration.AddUserSecrets<Program>();
builder.WebHost.ConfigureKestrel(options => { options.AddServerHeader = false; });

#endregion

Log.Logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .CreateLogger();

try
{
    #region AdminUI

    JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

    builder.Services
        .AddIdentityServer4AdminUI<AdminIdentityDbContext, IdentityServerConfigurationDbContext,
            IdentityServerPersistedGrantDbContext,
            AdminLogDbContext, AdminAuditLogDbContext, AuditLog, IdentityServerDataProtectionDbContext,
            UserIdentity, UserIdentityRole, UserIdentityUserClaim, UserIdentityUserRole,
            UserIdentityUserLogin, UserIdentityRoleClaim, UserIdentityUserToken, Guid,
            IdentityUserDto, IdentityRoleDto, IdentityUsersDto, IdentityRolesDto, IdentityUserRolesDto,
            IdentityUserClaimsDto, IdentityUserProviderDto, IdentityUserProvidersDto, IdentityUserChangePasswordDto,
            IdentityRoleClaimsDto, IdentityUserClaimDto, IdentityRoleClaimDto>(options =>
        {
            // Applies configuration from appsettings.
            options.BindConfiguration(builder.Configuration);

            builder.Services.TryAddSingleton(options.Http);

            options.Security.UseDeveloperExceptionPage = builder.Environment.IsDevelopment();
            options.Security.UseHsts = builder.Environment.IsDevelopment();

            // Set migration assembly for application of db migrations
            var migrationsAssembly =
                MigrationAssemblyConfiguration.GetMigrationAssemblyByProvider(options.DatabaseProvider);
            options.DatabaseMigrations.SetMigrationsAssemblies(migrationsAssembly);


            // Use production DbContexts and auth services.
            options.Testing.IsStaging = false;
        });

    // Monitor changes in Admin UI views
    builder.Services.AddAdminUiRazorRuntimeCompilation(builder.Environment);

    // Add email senders which is currently setup for SendGrid and SMTP
    builder.Services.AddEmailSenders(builder.Configuration);

    #endregion

    #region Serilog

    builder.Services.AddSerilog((services, loggerConfig) => loggerConfig
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.WithProperty("ApplicationName", builder.Environment.ApplicationName));
    builder.Host.UseSerilog();

    #endregion

    var app = builder.Build();

    var httpConfiguration = app.Services.GetRequiredService<HttpConfiguration>();
    var securityConfiguration = app.Services.GetRequiredService<SecurityConfiguration>();
    var adminConfiguration = app.Services.GetRequiredService<AdminConfiguration>();
    #region SecurityHeaders

    // Add custom security headers
    app.UseSecurityHeaders(securityConfiguration.CspTrustedDomains);

    #endregion

    #region BasePath

    if (!string.IsNullOrWhiteSpace(httpConfiguration.BasePath))
        app.UsePathBase(httpConfiguration.BasePath);

    #endregion

    app.UseCookiePolicy();

    if (securityConfiguration.UseDeveloperExceptionPage)
        app.UseDeveloperExceptionPage();
    else
        app.UseExceptionHandler("/Home/Error");

    if (securityConfiguration.UseHsts) app.UseHsts();

    #region Migrations


    var migrationComplete = await ApplyDbMigrationsWithDataSeedAsync(args, builder.Configuration, app.Services);
    if (args.Any(x => x == migrateOnlyArgs))
    {
        await app.StopAsync();
        if (!migrationComplete) Environment.ExitCode = -1;

        return;
    }

    #endregion

    #region Middleware


    app.UseStaticFiles();

    // Use Localization
    app.ConfigureLocalization();

    app.MapDefaultEndpoints();
    app.UseRouting();
    app.UseIdentityServer4AdminUi();
    app.MapIdentityServer4AdminUi();

    #endregion

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}


return;

static Task<bool> ApplyDbMigrationsWithDataSeedAsync(string[] args, IConfiguration configuration,
    IServiceProvider serviceProvider)
{
    var applyDbMigrationWithDataSeedFromProgramArguments = args.Any(x => x == seedArgs);
    if (applyDbMigrationWithDataSeedFromProgramArguments) args = args.Except([seedArgs]).ToArray();

    var seedConfiguration = configuration.GetSection(nameof(SeedConfiguration)).Get<SeedConfiguration>();
    var databaseMigrationsConfiguration = configuration.GetSection(nameof(DatabaseMigrationsConfiguration))
        .Get<DatabaseMigrationsConfiguration>();

    return DbMigrationHelpers
        .ApplyDbMigrationsWithDataSeedAsync<IdentityServerConfigurationDbContext, AdminIdentityDbContext,
            IdentityServerPersistedGrantDbContext, AdminLogDbContext, AdminAuditLogDbContext,
            IdentityServerDataProtectionDbContext, UserIdentity, UserIdentityRole>(serviceProvider,
            applyDbMigrationWithDataSeedFromProgramArguments, seedConfiguration, databaseMigrationsConfiguration);
}