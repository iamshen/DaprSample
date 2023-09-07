namespace DaprTool.BuildingBlocks.ApiExtensions.ApiResource.Dto;

/// <summary>
///     控制器发现 Dto
/// </summary>
public class Discovering
{
    /// <summary>
    ///     区域名称
    /// </summary>
    public string AreaName { get; set; } = string.Empty;

    /// <summary>
    ///     控制器名称
    /// </summary>
    public string ControllerName { get; set; } = string.Empty;

    /// <summary>
    ///     Action名称
    /// </summary>
    public string ActionName { get; set; } = string.Empty;

    /// <summary>
    ///     显示名称
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    ///     接口说明
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     路由模板
    /// </summary>
    public string? RouteTemplate { get; set; }

    /// <inheritdoc />
    public override string ToString()
    {
        return $"{ControllerName}.{ActionName}";
    }
}