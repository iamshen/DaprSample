using System.ComponentModel.DataAnnotations;
using DaprTool.BuildingBlocks.Utils.Data;
using DaprTool.BuildingBlocks.Utils.Enumerations;
using DaprTool.BuildingBlocks.Utils.ValueObjects;
using Microsoft.AspNetCore.Identity;

namespace DaprTool.BuildingBlocks.IdentityData;

/// <summary>
///     用户
/// </summary>
/// <remarks>用户基础信息直接保存用户表，其他第三方信息（微信、支付宝、抖音 openid、unionId），附属信息（注册地、会员等级..）使用 UserClaim 存储。</remarks>
public class UserBase : IdentityUser<Guid>, IUser
{
    /// <summary>
    ///     用户编码
    /// </summary>
    [Required]
    [StringLength(50)]
    public string UserCode { get; init; } = null!;

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