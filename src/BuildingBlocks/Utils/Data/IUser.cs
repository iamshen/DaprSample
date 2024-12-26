using System.ComponentModel.DataAnnotations;
using DaprTool.BuildingBlocks.Utils.Enumerations;
using DaprTool.BuildingBlocks.Utils.ValueObjects;

namespace DaprTool.BuildingBlocks.Utils.Data;

/// <summary>
///     用户接口
/// </summary>
public interface IUser
{
    /// <summary>
    ///     编码
    /// </summary>
    [Required]
    [StringLength(50)]
    public string UserCode { get; init; }

    /// <summary>
    ///     昵称
    /// </summary>
    [StringLength(200)]
    public string? NickName { get; init; }

    /// <summary>
    ///     头像
    /// </summary>
    [StringLength(1000)]
    public string? Avatar { get; init; }

    /// <summary>
    ///     账户状态
    /// </summary>
    public AccountStatus AccountStatus { get; init; }

    /// <summary>
    ///     实名认证信息
    /// </summary>
    public RealNameAuthentication? Authentication { get; init; }

    /// <summary>
    ///     实名认证状态
    /// </summary>
    public AuthenticationStatus AuthenticationStatus { get; init; }

    /// <summary>
    ///     注册时间
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