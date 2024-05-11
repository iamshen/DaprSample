using Idsrv4.Admin.Shared.ModuleInitializer;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Logging;

var builder = WebApplication.CreateBuilder();

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
    #region Configuration

    IRootConfiguration rootConfiguration = new RootConfiguration();
    builder.Configuration.GetSection(ConfigurationConsts.AdminConfigurationKey).Bind(rootConfiguration.AdminConfiguration);
    builder.Configuration.GetSection(ConfigurationConsts.RegisterConfigurationKey).Bind(rootConfiguration.RegisterConfiguration);
    builder.Services.AddSingleton(rootConfiguration);

    #endregion

    #region Services

    // Register DbContexts for IdentityServer and Identity
    builder.Services
        .RegisterDbContexts<AdminIdentityDbContext, IdentityServerConfigurationDbContext,
            IdentityServerPersistedGrantDbContext, IdentityServerDataProtectionDbContext>(builder.Configuration);

    // Save data protection keys to db, using a common application name shared between Admin and STS
    builder.Services.AddDataProtection<IdentityServerDataProtectionDbContext>();

    // Add email senders which is currently setup for SendGrid and SMTP
    builder.Services.AddEmailSenders(builder.Configuration);

    // Add services for authentication, including Identity model and external providers
    builder.Services.AddAuthenticationServices<AdminIdentityDbContext, UserIdentity, UserIdentityRole>(
        builder.Configuration);
    builder.Services
        .AddIdentityServer<IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext, UserIdentity>(
            builder.Configuration);

    // Add HSTS options
    builder.Services.AddHsts(options =>
    {
        options.Preload = true;
        options.IncludeSubDomains = true;
        options.MaxAge = TimeSpan.FromDays(365);
    });

    // Add all dependencies for Asp.Net Core Identity in MVC - these dependencies are injected into generic Controllers
    // Including settings for MVC and Localization
    // If you want to change primary keys or use another db model for Asp.Net Core Identity:
    builder.Services.AddMvcWithLocalization<UserIdentity, string>(builder.Configuration);

    // Add authorization policies for MVC
    builder.Services.AddAuthorizationPolicies(rootConfiguration);

    builder.Services
        .AddIdSHealthChecks<IdentityServerConfigurationDbContext, IdentityServerPersistedGrantDbContext,
            AdminIdentityDbContext, IdentityServerDataProtectionDbContext>(builder.Configuration);

    #endregion

    #region Serilog

    builder.Services.AddSerilog((_, loggerConfig) => loggerConfig
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.WithProperty("ApplicationName", builder.Environment.ApplicationName));
    builder.Host.UseSerilog();

    #endregion

    var app = builder.Build();
    IdentityModelEventSource.ShowPII = true;
    // Add custom security headers
    app.UseSecurityHeaders(builder.Configuration);
    
   #region BasePath

     string basePath = builder.Configuration.GetValue<string>("BasePath");
    if (!string.IsNullOrWhiteSpace(basePath))
            app.UsePathBase(new PathString(basePath));

   #endregion
            
    app.UseCookiePolicy();

    if (app.Environment.IsDevelopment())
        app.UseDeveloperExceptionPage();
    else
        app.UseHsts();


    app.UseStaticFiles();
    app.UseIdentityServer();


    app.UseMvcLocalizationServices();

    app.UseRouting();
    app.UseAuthorization();

    app.MapDefaultEndpoints();
    app.MapDefaultControllerRoute();
    

    await app.RunAsync();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Sts Server terminated unexpectedly");
}
finally
{
    await Log.CloseAndFlushAsync();
}

