namespace Idsrv4.Admin.UI.Configuration;

public class AdminConfiguration
{
    public string PageTitle { get; set; }
    public string FaviconUri { get; set; }
    public string[] Scopes { get; set; }
    public string AdministrationRole { get; set; }
    public bool RequireHttpsMetadata { get; set; }
    public string IdentityAdminCookieName { get; set; }
    public double IdentityAdminCookieExpiresUtcHours { get; set; }
    public string TokenValidationClaimName { get; set; }
    public string TokenValidationClaimRole { get; set; }
    public string IdentityAdminRedirectPath { get; set; }
    public string IdentityServerBasePath { get; set; }
    public string WebAdminBasePath { get; set; }
    public string ProxyServerUrl { get; set; }
    public string IdentityServerBaseUrl => ProxyServerUrl + IdentityServerBasePath;
    public string WebAdminBaseUrl => ProxyServerUrl + WebAdminBasePath;
    public string IdentityAdminRedirectUri => ProxyServerUrl + IdentityAdminRedirectPath;
    public string ClientId { get; set; }
    public string ClientSecret { get; set; }
    public string OidcResponseType { get; set; }
    public bool HideUIForMSSqlErrorLogging { get; set; }
    public string Theme { get; set; }
    public string CustomThemeCss { get; set; }
}