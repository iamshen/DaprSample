using IdentityModel.AspNetCore.AccessTokenManagement;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.FluentUI.AspNetCore.Components;
using Microsoft.IdentityModel.Tokens;
using WebAdmin.Components;
using WebAdmin.Plumbing;
using WebAdmin.Shared.Configurations;
using WebAdmin.Shared.Infrastructure;
using _Imports = WebAdmin.Client._Imports;

var builder = WebApplication.CreateBuilder(args);

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

// �ο�����
// https://github.com/IdentityModel/IdentityModel.AspNetCore/blob/72479bf781eac07b5f7f568ae45e498b5ba9ed69/samples/BlazorServer/HostingExtensions.cs#L15
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
        })
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, options =>
    {
        options.Authority = adminConfiguration.IdentityServerBaseUrl;
        options.RequireHttpsMetadata = adminConfiguration.RequireHttpsMetadata;
        options.ClientId = adminConfiguration.ClientId;
        options.ClientSecret = adminConfiguration.ClientSecret;
        options.ResponseType = adminConfiguration.OidcResponseType!;


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
            }
        };
    });

// ��� access token ����
builder.Services.AddAccessTokenManagement();
// �޷��ڻỰ��session���й������� ��Ϊ �� Blazor �������в������Ա�̷�ʽʹ�� HttpContext
builder.Services.AddSingleton<IUserAccessTokenStore, ServerSideTokenStore>();


builder.Services.AddAuthorization(options =>
{
    // Ĭ������£��������󶼽�����Ĭ�ϲ��Խ�����Ȩ ���������û�����������¼/ע���������̣�ע�͵�
    options.FallbackPolicy = options.DefaultPolicy;
});


var app = builder.Build();

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