using Microsoft.AspNetCore.Identity;

namespace DaprTool.BuildingBlocks.IdentityData;

public class RoleBase : IdentityRole<Guid>
{
    /// <summary>
    ///     备注
    /// </summary>
    public string? Description { get; init; }

    /// <summary>   
    ///     创建时间
    /// </summary>
    public DateTimeOffset CreatedTime { get; init; }

    /// <summary>
    ///     更新时间
    /// </summary>
    public DateTimeOffset UpdatedTime { get; init; }

    /// <summary>
    ///     逻辑删除时间
    /// </summary>
    public DateTimeOffset DeletedTime { get; init; }
}