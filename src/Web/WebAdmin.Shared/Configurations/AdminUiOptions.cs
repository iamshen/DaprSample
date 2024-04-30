using Microsoft.Extensions.Configuration;

namespace WebAdmin.Shared.Configurations;

/// <summary>
///     管理后台配置
/// </summary>
public class AdminUiOptions
{
    /// <summary> 管理后台的基础配置 </summary>
    public AdminConfiguration Admin { get; } = new();

    /// <summary> 全球化配置 </summary>
    public CultureConfiguration Culture { get; set; } = new();

    /// <summary>   HTTP 托管环境配置 </summary>
    public HttpConfiguration Http { get; set; } = new();

    /// <summary>
    ///     将从 appsettings 文件解析的配置应用到这些选项中。
    /// </summary>
    /// <param name="configuration">绑定到此实例的配置</param>
    public void BindConfiguration(IConfiguration configuration)
    {
        configuration.GetSection(nameof(AdminConfiguration)).Bind(Admin);
        configuration.GetSection(nameof(CultureConfiguration)).Bind(Culture);
        configuration.GetSection(nameof(HttpConfiguration)).Bind(Http);
    }
}

/// <summary>
///     管理后台的基础配置
/// </summary>
public class AdminConfiguration
{
    /// <summary> Page Name </summary>
    public string? PageTitle { get; set; }

    /// <summary> Favicon Uri </summary>
    public string? FaviconUri { get; set; }

    /// <summary> Web Admin Redirect Uri </summary>
    public string? AdminRedirectUri { get; set; }

    /// <summary> Scopes </summary>
    public string[] Scopes { get; set; } = [];

    /// <summary> Administration Role Name</summary>
    public string? AdministrationRole { get; set; }

    /// <summary> Require HttpsMetadata </summary>
    public bool RequireHttpsMetadata { get; set; }

    /// <summary> Web Admin CookieName </summary>
    public string? AdminCookieName { get; set; }

    /// <summary> Web Admin Cookie Expires UtcHours </summary>
    public double AdminCookieExpiresUtcHours { get; set; }

    /// <summary> Token Validation ClaimName </summary>
    public string? TokenValidationClaimName { get; set; }

    /// <summary> Token Validation ClaimRole </summary>
    public string? TokenValidationClaimRole { get; set; }

    /// <summary> IdentityServer BaseUrl </summary>
    public string? IdentityServerBaseUrl { get; set; }

    /// <summary> ClientId </summary>
    public string? ClientId { get; set; }

    /// <summary> ClientSecret </summary>
    public string? ClientSecret { get; set; }

    /// <summary> Oidc ResponseType </summary>
    public string? OidcResponseType { get; set; }
}

/// <summary>
///     全球化配置
/// </summary>
public class CultureConfiguration
{
    /// <summary> 可用的多语言文化 </summary>
    public static readonly string[] AvailableCultures = ["zh-Hans", "en"];

    /// <summary> 默认的文化 </summary>
    public static readonly string DefaultRequestCulture = "zh-Hans";

    /// <summary> 可用的多语言文化列表 </summary>
    public List<string> Cultures { get; set; } = [];

    /// <summary> 默认的文化 </summary>
    public string DefaultCulture { get; set; } = DefaultRequestCulture;
}

/// <summary>
///     Http 配置
/// </summary>
public class HttpConfiguration
{
    /// <summary> BasePath </summary>
    public string BasePath { get; set; } = "";
}