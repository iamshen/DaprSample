namespace DaprTool.BuildingBlocks.ApiExtensions.ApiResource.Dto;

/// <summary>
///     Api Resource Dto
/// </summary>
public class ApiResource
{
    /// <summary>
    ///     api 资源名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     api 资源名称 显示名称
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    ///     资源描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     Api 作用域
    /// </summary>
    public List<ApiResourceScope> ApiResourceScopes { get; set; } = new();
}