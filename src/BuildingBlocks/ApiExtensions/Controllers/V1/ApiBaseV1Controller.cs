using Asp.Versioning;
using DaprTool.BuildingBlocks.Utils.Extensions;
using DaprTool.BuildingBlocks.Utils.ValueObjects;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.AspNetCore.Mvc;

#region 接口基类控制器

/// <summary>
///     接口基类控制器
/// </summary>
[Route("[controller]/[action]")]
[ApiController]
[ApiVersion("1.0")]
public abstract class ApiBaseV1Controller : ControllerBase
{
    #region 属性

    /// <summary>
    ///     日志对象
    /// </summary>
    protected ILogger Logger
    {
        get
        {
            var loggerFactory = HttpContext.RequestServices.GetService<ILoggerFactory>();
            return loggerFactory!.CreateLogger(typeof(ILogger<>).MakeGenericType(GetType()));
        }
    }

    /// <summary>
    ///     服务提供者
    /// </summary>
    protected IServiceProvider ServiceProvider => HttpContext.RequestServices;

    /// <summary>
    ///     当前用户
    /// </summary>
    protected IdentityUserInfo UserInfo => User.Identity.GetUserInfo();


    #region 获取操作员

    /// <summary>
    ///     获取操作员
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="isThrowExceptionIfNotFound"></param>
    /// <returns></returns>
    protected OperatorObject<T>? GetOperator<T>(bool isThrowExceptionIfNotFound = false) where T : IEquatable<T>
    {
        try
        {
            return new OperatorObject<T>
            {
                OperatorId = UserInfo.UserId.CastTo<T>(),
                OperatorUserName = UserInfo.UserName,
                OperatorRealName = UserInfo.RealName
            };
        }
        catch
        {
            if (isThrowExceptionIfNotFound)
                throw;
            return null;
        }
    }

    #endregion

    #endregion
}

#endregion