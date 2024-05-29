using System.IdentityModel.Tokens.Jwt;
using IdentityModel.AspNetCore.AccessTokenManagement;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using OpenTelemetry.Exporter;
using WebAdmin.Components;
using WebAdmin.Plumbing;
using WebAdmin.Shared.Configurations;
using WebAdmin.Shared.Infrastructure;
using _Imports = WebAdmin.Client._Imports;
IdentityModelEventSource.ShowPII = true;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddProblemDetails();

builder.AddServiceDefaults();

var adminUiOptions = new AdminUiOptions();

adminUiOptions.BindConfiguration(builder.Configuration);
builder.Services.AddSingleton(adminUiOptions);
builder.Services.AddSingleton(adminUiOptions.Admin);
builder.Services.AddSingleton(adminUiOptions.Culture);
builder.Services.AddSingleton(adminUiOptions.Http);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddLocalization(opts => opts.ResourcesPath = ConfigurationConsts.ResourcesPath);
builder.Services.AddAdminUiServerServices();
builder.Services.AddFluentUIComponents();
builder.Services.AddControllers();

var adminConfiguration = adminUiOptions.Admin;

// 参考链接
// https://github.com/IdentityModel/IdentityModel.AspNetCore/blob/72479bf781eac07b5f7f568ae45e498b5ba9ed69/samples/BlazorServer/HostingExtensions.cs#L15
JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
builder.Services.AddAuthentication(options =>
    {
        options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
        options.DefaultSignOutScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
        options =>
        {
            options.Cookie.Name = adminConfiguration.AdminCookieName;
            options.SessionStore = new CookieSessionStore();
        })
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.Authority = adminConfiguration.IdentityServerBaseUrl;
        options.RequireHttpsMetadata = adminConfiguration.RequireHttpsMetadata;
        options.ClientId = adminConfiguration.ClientId;
        options.ClientSecret = adminConfiguration.ClientSecret;
        options.ResponseType = adminConfiguration.OidcResponseType!;
        options.MapInboundClaims = false;

        options.Scope.Clear();
        foreach (var scope in adminConfiguration.Scopes) options.Scope.Add(scope);

        options.ClaimActions.MapJsonKey(adminConfiguration.TokenValidationClaimRole!,
            adminConfiguration.TokenValidationClaimRole!,
            adminConfiguration.TokenValidationClaimRole!);

        options.SaveTokens = true;

        options.GetClaimsFromUserInfoEndpoint = true;

        options.TokenValidationParameters = new TokenValidationParameters
        {
            NameClaimType = adminConfiguration.TokenValidationClaimName,
            RoleClaimType = adminConfiguration.TokenValidationClaimRole
        };

        options.Events = new OpenIdConnectEvents
        {
            OnMessageReceived = context =>
            {
                if (context.Properties is null) return Task.CompletedTask;

                context.Properties.IsPersistent = true;
                context.Properties.ExpiresUtc =
                    new DateTimeOffset(DateTime.Now.AddHours(adminConfiguration.AdminCookieExpiresUtcHours));
                return Task.CompletedTask;
            },
            OnRedirectToIdentityProvider = context =>
            {
                if (!string.IsNullOrEmpty(adminConfiguration.AdminRedirectUri))
                    context.ProtocolMessage.RedirectUri = adminConfiguration.AdminRedirectUri;

                return Task.CompletedTask;
            },
            OnTokenValidated = async n =>
            {
                var svc = n.HttpContext.RequestServices.GetRequiredService<IUserAccessTokenStore>();
                var exp = DateTimeOffset.UtcNow.AddSeconds(Double.Parse(n.TokenEndpointResponse!.ExpiresIn));

                if(n.Principal is not null)
                    await svc.StoreTokenAsync(n.Principal, n.TokenEndpointResponse.AccessToken, exp,
                        n.TokenEndpointResponse.RefreshToken);
            }
        };
    });

// 添加 access token 管理
builder.Services.AddAccessTokenManagement();
// 无法在会话（session）中管理令牌 因为 在 Blazor 服务器中不允许以编程方式使用 HttpContext
builder.Services.AddSingleton<IUserAccessTokenStore, ServerSideTokenStore>();
builder.Services.AddAntiforgery();

builder.Services.AddAuthorization(options =>
{
    // 默认情况下，所有请求都将根据默认策略进行授权 如果您想从用户界面驱动登录/注销工作流程，注释掉
    options.FallbackPolicy = options.DefaultPolicy;
});


var app = builder.Build();

app.MapDefaultEndpoints();

#region BasePath

if (!string.IsNullOrWhiteSpace(adminUiOptions.Http.BasePath))
{
    app.UsePathBase(new PathString(adminUiOptions.Http.BasePath));
}

#endregion

#region ForwardedHeaders

// forward
var forwardingOptions = new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
};

forwardingOptions.KnownNetworks.Clear();
forwardingOptions.KnownProxies.Clear();

app.UseForwardedHeaders(forwardingOptions);

app.UseXXssProtection(options => options.EnabledWithBlockMode());
app.UseXContentTypeOptions();
app.UseXfo(options => options.SameOrigin());
app.UseReferrerPolicy(options => options.NoReferrer());

#endregion

app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All,
    KnownNetworks = { },
    KnownProxies = { }
});

app.UseWebSockets();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.MapControllers();
app.UseRequestLocalization(options =>
{
    var cultureConfiguration = adminUiOptions.Culture;
    var supportedCultureCodes =
        (cultureConfiguration?.Cultures?.Count > 0
            ? cultureConfiguration.Cultures.Intersect(CultureConfiguration.AvailableCultures)
            : CultureConfiguration.AvailableCultures).ToArray();
    if (!supportedCultureCodes.Any())
        supportedCultureCodes = CultureConfiguration.AvailableCultures;
    var defaultCultureCode = string.IsNullOrEmpty(cultureConfiguration?.DefaultCulture)
        ? CultureConfiguration.DefaultRequestCulture
        : cultureConfiguration?.DefaultCulture;
    if (!supportedCultureCodes.Contains(defaultCultureCode))
        defaultCultureCode = supportedCultureCodes.FirstOrDefault();

    options.AddSupportedCultures(supportedCultureCodes)
        .AddSupportedUICultures(supportedCultureCodes)
        .SetDefaultCulture(defaultCultureCode!);
});
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(_Imports).Assembly);

app.Run();