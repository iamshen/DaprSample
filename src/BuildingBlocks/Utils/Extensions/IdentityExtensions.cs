#nullable disable

using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;
using DaprTool.BuildingBlocks.Utils.ValueObjects;

namespace DaprTool.BuildingBlocks.Utils.Extensions;

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
