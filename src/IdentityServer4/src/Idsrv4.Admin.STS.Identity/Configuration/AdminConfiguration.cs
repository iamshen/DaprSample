namespace Idsrv4.Admin.STS.Identity.Configuration;

public class AdminConfiguration
{
    public string PageTitle { get; set; }
    public string HomePageLogoUri { get; set; }
    public string FaviconUri { get; set; }
    public string IdentityAdminBasePath { get; set; }
    public string WebAdminBasePath { get; set; }
    public string ProxyServerUrl { get; set; }
    public string IdentityAdminBaseUrl => ProxyServerUrl + IdentityAdminBasePath;
    public string WebAdminBaseUrl => ProxyServerUrl + WebAdminBasePath;
    public string AdministrationRole { get; set; }

    public string Theme { get; set; }

    public string CustomThemeCss { get; set; }
}