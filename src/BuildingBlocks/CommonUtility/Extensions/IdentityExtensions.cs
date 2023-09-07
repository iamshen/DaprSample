#nullable disable

using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;

namespace GoldCloud.Infrastructure.Common.Extensions;

/// <summary>
///     身份标识扩展方法
/// </summary>
public static class IdentityExtensions
{
    /// <summary>
    ///     从声明的身份标识中读取用户信息
    /// </summary>
    /// <param name="identity"> </param>
    /// <returns> </returns>
    public static IdentityUserInfo GetUserInfo(this IIdentity identity)
    {
        const string unauthorized = "用户未登录";

        if (identity == null) throw new Exception(unauthorized);

        if (identity is not ClaimsIdentity claimsIdentity) throw new Exception(unauthorized);

        var userId = claimsIdentity.FindFirst("UserId")?.Value;

        if (string.IsNullOrWhiteSpace(userId)) throw new Exception(unauthorized);

        var user = new IdentityUserInfo
        {
            UserId = userId,
            UserName = claimsIdentity.FindFirst("UserName")?.Value,
            UserType = claimsIdentity.FindFirst("UserType")?.Value,
            DisplayName = claimsIdentity.FindFirst("DisplayName")?.Value,
            RealName = claimsIdentity.FindFirst("RealName")?.Value,
            Email = claimsIdentity.FindFirst("Email")?.Value,
            PhoneNumber = claimsIdentity.FindFirst("PhoneNumber")?.Value,
            Avatar = claimsIdentity.FindFirst("Avatar")?.Value,
            AccountStatus = claimsIdentity.FindFirst("AccountStatus")?.Value,
            AuthenticationStatus = claimsIdentity.FindFirst("AuthenticationStatus")?.Value
        };

        var roles = claimsIdentity.FindAll(x => x.Type == "UserRoles");
        var enumerable = roles as Claim[] ?? roles.ToArray();
        if (!enumerable.Any()) return user;

        var roleInfos = enumerable.Select(x => JsonSerializer.Deserialize<IdentityRoleInfo>(x.Value)).ToList();
        if (roleInfos.Any()) user.Roles.AddRange(roleInfos);

        return user;
    }
}

/// <summary>
///     IdentityUserInfo
/// </summary>
public class IdentityUserInfo
{
    /// <summary>
    ///     获取或设置 用户主键
    /// </summary>
    public string UserId { get; set; }

    /// <summary>
    ///     获取或设置 用户名
    /// </summary>
    public string UserName { get; set; }

    /// <summary>
    ///     获取或设置 显示名称
    /// </summary>
    public string DisplayName { get; set; }

    /// <summary>
    ///     获取或设置 真实名称
    /// </summary>
    public string RealName { get; set; }

    /// <summary>
    ///     头像
    /// </summary>
    public string Avatar { get; set; }

    /// <summary>
    ///     账号状态
    /// </summary>
    public string AccountStatus { get; set; }

    /// <summary>
    ///     认证状态
    /// </summary>
    public string AuthenticationStatus { get; set; }

    /// <summary>
    ///     获取或设置 电子邮箱
    /// </summary>
    public string Email { get; set; }

    /// <summary>
    ///     获取或设置 手机号码
    /// </summary>
    public string PhoneNumber { get; set; }

    /// <summary>
    ///     OpenId
    /// </summary>
    public string OpenId { get; set; }

    /// <summary>
    ///     用户类型 1会员、2员工
    /// </summary>
    public string UserType { get; set; }

    /// <summary>
    ///     Roles
    /// </summary>
    public List<IdentityRoleInfo> Roles { get; set; } = new();
}

/// <summary>
///     IdentityRoleInfo
/// </summary>
public class IdentityRoleInfo
{
    /// <summary>
    ///     id
    /// </summary>
    public string Id { get; set; }

    /// <summary>
    ///     名称
    /// </summary>
    public string Name { get; set; }
}