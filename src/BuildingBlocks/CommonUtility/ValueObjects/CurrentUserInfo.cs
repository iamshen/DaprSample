namespace DaprTool.BuildingBlocks.CommonUtility.ValueObjects;

public record IdentityUserInfo
{
    /// <summary>
    ///     用户主键
    /// </summary>
    public string UserId { get; set; } = string.Empty;

    /// <summary>
    ///     用户名
    /// </summary>
    public string UserName { get; set; } = string.Empty;

    /// <summary>
    ///     显示名称
    /// </summary>
    public string DisplayName { get; set; } = string.Empty;

    /// <summary>
    ///     真实名称
    /// </summary>
    public string RealName { get; set; } = string.Empty;

    /// <summary>
    ///     头像
    /// </summary>
    public string Avatar { get; set; } = string.Empty;

    /// <summary>
    ///     账号状态
    /// </summary>
    public string AccountStatus { get; set; } = string.Empty;

    /// <summary>
    ///     认证状态
    /// </summary>
    public string AuthenticationStatus { get; set; } = string.Empty;

    /// <summary>
    ///     电子邮箱
    /// </summary>
    public string Email { get; set; } = string.Empty;

    /// <summary>
    ///     手机号码
    /// </summary>
    public string PhoneNumber { get; set; } = string.Empty;

    /// <summary>
    ///     OpenId
    /// </summary>
    public string OpenId { get; set; } = string.Empty;

    /// <summary>
    ///     用户类型
    /// </summary>
    public string UserType { get; set; } = string.Empty;

    /// <summary>
    ///     Roles
    /// </summary>
    public List<IdentityRoleInfo> Roles { get; set; } = new();
}

/// <summary>
///     IdentityRoleInfo
/// </summary>
public record IdentityRoleInfo
{
    /// <summary>
    ///     id
    /// </summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>
    ///     名称
    /// </summary>
    public string Name { get; set; } = string.Empty;
}