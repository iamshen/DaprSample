namespace DaprTool.BuildingBlocks.ApiExtensions.ApiResource.Dto;

/// <summary>
///     Api 作用域
/// </summary>
public record ApiResourceScope
{
    /// <summary>
    ///     名称
    /// </summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>
    ///     显示名称
    /// </summary>
    public string? DisplayName { get; set; }

    /// <summary>
    ///     描述
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    ///     用户声明
    /// </summary>
    public List<string> UserClaims { get; set; } = new() { "UserId" };

}