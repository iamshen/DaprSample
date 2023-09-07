namespace Microsoft.Extensions.DependencyInjection.Options;

/// <summary>
///     api 资源配置项
/// </summary>
public class ApiResourceOptions
{
    /// <summary>
    ///     是否自动注册 API 资源到 IdentityServer4
    /// </summary>
    public bool AutoRegisterApiResource { get; set; }

    /// <summary>
    ///     资源名称
    /// </summary>
    public string ResourceName { get; set; } = string.Empty;

    /// <summary>
    ///     资源显示名称
    /// </summary>
    public string ResourceDisplayName { get; set; } = string.Empty;

    /// <summary>
    ///     资源的描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     ids4 api url
    ///     <example>https://develop.api.inglod.net</example>
    /// </summary>
    public string IdentityServerApiDomain { get; set; } = string.Empty;
}