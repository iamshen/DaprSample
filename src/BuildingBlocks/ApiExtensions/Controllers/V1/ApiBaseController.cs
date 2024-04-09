using Microsoft.AspNetCore.Authorization;

namespace Microsoft.AspNetCore.Mvc;

#region 接口基类控制器

/// <summary>
///     接口基类控制器
/// </summary>
[AllowAnonymous]
[Route("[controller]/[action]")]
public class ApiBaseController : ApiBaseV1Controller
{
}

#endregion

#region 各种基类控制器

/// <summary>
///     【移动端】接口基类控制器
///     <remarks>
///         <para>移动端路由：[Route("Mobile/[controller]/[action]")]</para>
///         <para>若继承此控制器，认证方式为：用户身份认证。</para>
///         <para>请在继承的控制器类中使用特性 <see cref="ApiExplorerSettingsAttribute" /> 指定分组名称 mobile，以便于Swagger 分组。</para>
///     </remarks>
/// </summary>
[Route("Mobile/[controller]")]
[ApiExplorerSettings(GroupName = "mobile")]
public abstract class MobileApiBaseController : ApiBaseController
{
}

/// <summary>
///     【开放】接口基类控制器
///     <remarks>
///         <para>移动端路由：[Route("Open/[controller]/[action]")]</para>
///         <para>若继承此控制器，认证方式为：匿名，不进行身份认证。</para>
///         <para>请在继承的控制器类中使用特性 <see cref="ApiExplorerSettingsAttribute" /> 指定分组名称 open，以便于Swagger 分组。</para>
///     </remarks>
/// </summary>
[AllowAnonymous]
[Route("Open/[controller]")]
[ApiExplorerSettings(GroupName = "open")]
public abstract class OpenApiBaseController : ApiBaseController
{
}

#endregion 接口基类控制器