namespace DaprTool.BuildingBlocks.ApiSwagger;

#region Swagger配置

/// <summary>
///     Swagger配置
/// </summary>
public class SwaggerOptions
{
    /// <summary>
    ///     swagger终结点
    /// </summary>
    public IList<SwaggerEndpoint> Endpoints { get; set; } = new List<SwaggerEndpoint>();

    /// <summary>
    ///     路由前缀
    /// </summary>
    public string RoutePrefix { get; set; } = "docs";

    /// <summary>
    ///     是否隐藏schemas
    /// </summary>
    public bool IsHideSchemas { get; set; }

    /// <summary>
    ///     是否启用swagger
    /// </summary>
    public bool Enabled { get; set; } = true;
}

#endregion

#region swagger终结点

/// <summary>
///     swagger终结点
/// </summary>
public class SwaggerEndpoint
{
    /// <summary>
    ///     标题
    /// </summary>
    public string Title { get; set; } = string.Empty;

    /// <summary>
    ///     名称
    /// </summary>
    public string Name { get; set; } = string.Empty;
}

#endregion