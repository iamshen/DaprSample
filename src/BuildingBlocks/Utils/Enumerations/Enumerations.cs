using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace DaprTool.BuildingBlocks.Utils;

#region 错误编码

/// <summary>
/// 错误编码
/// </summary>
public enum ErrorCode : ushort
{
    /// <summary>
    /// 成功
    /// </summary>
    [Display(Name = "成功")]
    [EnumMember(Value = "Success")] Success = 2000,

    /// <summary>
    /// 未知错误
    /// </summary>
    [Display(Name = "未知错误")]
    [EnumMember(Value = "Unknown")] Unknown = 0,

    /// <summary>
    /// 操作超时
    /// </summary>
    [Display(Name = "操作超时")]
    [EnumMember(Value = "OperationTimedout")] OperationTimedout = 3000,

    /// <summary>
    /// 不支持的版本号
    /// </summary>
    [Display(Name = "不支持的版本号")]
    [EnumMember(Value = "VersionsUnsupported")] VersionsUnsupported = 3001,

    /// <summary>
    /// 无效参数
    /// </summary>
    [Display(Name = "无效参数")]
    [EnumMember(Value = "InvalidArgument")] InvalidArgument = 3002,

    /// <summary>
    /// 未授权
    /// </summary>
    [Display(Name = "未授权")]
    [EnumMember(Value = "Unauthorized")] Unauthorized = 3003,

    /// <summary>
    /// 禁止操作
    /// </summary>
    [Display(Name = "禁止操作")]
    [EnumMember(Value = "Forbidden")] Forbidden = 3004,

    /// <summary>
    /// 资源未找到
    /// </summary>
    [Display(Name = "资源未找到")]
    [EnumMember(Value = "ResourcesNotFound")] ResourcesNotFound = 3005,

    /// <summary>
    /// 服务不可用
    /// </summary>
    [Display(Name = "服务不可用")]
    [EnumMember(Value = "ServiceUnavailable")] ServiceUnavailable = 3006,

    /// <summary>
    /// 参数异常
    /// </summary>
    [Display(Name = "参数异常")]
    [EnumMember(Value = "ArgumentException")] ArgumentException = 3007,

    /// <summary>
    /// 对象不能初始化
    /// </summary>
    [Display(Name = "对象不能初始化")]
    [EnumMember(Value = "ObjectCannotInitialized")] ObjectCannotInitialized = 3008,

    /// <summary>
    /// 对象已经存在
    /// </summary>
    [Display(Name = "对象已经存在")]
    [EnumMember(Value = "ObjectAlreadyExists")] ObjectAlreadyExists = 3009,

    /// <summary>
    /// 不支持的交易类型
    /// </summary>
    [Display(Name = "不支持的交易类型")]
    [EnumMember(Value = "NotSupportTradingType")] NotSupportTradingType = 3010,

    /// <summary>
    /// 超出范围
    /// </summary>
    [Display(Name = "超出范围")]
    [EnumMember(Value = "OutOfRange")] OutOfRange = 3011,

    /// <summary>
    /// 对象不存在
    /// </summary>
    [Display(Name = "对象不存在")]
    [EnumMember(Value = "ObjectNotExists")] ObjectNotExists = 3014,

    /// <summary>
    /// 不支持的资源类型
    /// </summary>
    [Display(Name = "不支持的资源类型")]
    [EnumMember(Value = "UnsupportedResourceType")] UnsupportedResourceType = 3015,

    /// <summary>
    /// 操作太频繁
    /// </summary>
    [Display(Name = "操作太频繁")]
    [EnumMember(Value = "OperatingTooAften")] OperatingTooAften = 3016,

    /// <summary>
    /// 签名验证失败
    /// </summary>
    [Display(Name = "签名验证失败")]
    [EnumMember(Value = "SignatureValidateFailed")] SignatureValidateFailed = 3017,

    /// <summary>
    /// 丢失参数
    /// </summary>
    [Display(Name = "丢失参数")]
    [EnumMember(Value = "MissingParameter")] MissingParameter = 3018,

    /// <summary>
    /// 配置未找到
    /// </summary>
    [Display(Name = "配置未找到")]
    [EnumMember(Value = "ConfigNotFound")] ConfigNotFound = 3019,

    /// <summary>
    /// 订单状态异常
    /// </summary>
    [Display(Name = "订单状态异常")]
    [EnumMember(Value = "OrderStatusAbnormal")] OrderStatusAbnormal = 3020,

    /// <summary>
    /// 数据未找到
    /// </summary>
    [Display(Name = "数据未找到")]
    [EnumMember(Value = "DataNotFound")] DataNotFound = 3021,

    /// <summary>
    /// 付款状态异常
    /// </summary>
    [Display(Name = "付款状态异常")]
    [EnumMember(Value = "PaymentStatusAbnormal")] PaymentStatusAbnormal = 3022,

    /// <summary>
    /// 收款账户异常
    /// </summary>
    [Display(Name = "收款账户异常")]
    [EnumMember(Value = "PayeeAccountAbnormal")] PayeeAccountAbnormal = 3023,

    /// <summary>
    /// 订单已取消
    /// </summary>
    [Display(Name = "订单已取消")]
    [EnumMember(Value = "OrderCancelled")] OrderCancelled = 3024,

    /// <summary>
    /// 审核未通过
    /// </summary>
    [Display(Name = "审核未通过")]
    [EnumMember(Value = "Unapprove")] Unapprove = 3025,

    /// <summary>
    /// 未找到付款服务
    /// </summary>
    [Display(Name = "未找到付款服务")]
    [EnumMember(Value = "NotFoundPaymentService")] NotFoundPaymentService = 3026,

    /// <summary>
    /// 数额超出范围
    /// </summary>
    [Display(Name = "数额超出范围")]
    [EnumMember(Value = "AmountOutOfRange")] AmountOutOfRange = 3027,

    /// <summary>
    /// 付款失败
    /// </summary>
    [Display(Name = "付款失败")]
    [EnumMember(Value = "PaymentFailed")] PaymentFailed = 3028,

    /// <summary>
    /// 收款账户无效
    /// </summary>
    [Display(Name = "收款账户无效")]
    [EnumMember(Value = "PayeeAccountInvalid")] PayeeAccountInvalid = 3029,

    /// <summary>
    /// 收款账户信息不匹配
    /// </summary>
    [Display(Name = "收款账户信息不匹配")]
    [EnumMember(Value = "PayeeAccountMismatch")] PayeeAccountMismatch = 3030,

    /// <summary>
    /// 付款状态不明确
    /// </summary>
    [Display(Name = "付款状态不明确")]
    [EnumMember(Value = "PaymentStatusUndefined")] PaymentStatusUndefined = 3031,

    /// <summary>
    /// 操作失败
    /// </summary>
    [Display(Name = "操作失败")]
    [EnumMember(Value = "OperationFailed")] OperationFailed = 3032,

    /// <summary>
    /// 订单不存在
    /// </summary>
    [Display(Name = "订单不存在")]
    [EnumMember(Value = "OrderNotExists")] OrderNotExists = 3033,


    /// <summary>
    /// 信息不一致
    /// </summary>
    [Display(Name = "信息不一致")]
    [EnumMember(Value = "InformationInconsistency")] InformationInconsistency = 3035,

    /// <summary>
    /// 身份验证失败
    /// </summary>
    [Display(Name = "身份验证失败")]
    [EnumMember(Value = "AuthenticationFailed")] AuthenticationFailed = 3036,

    /// <summary>
    /// 网络错误
    /// </summary>
    [Display(Name = "网络错误")]
    [EnumMember(Value = "NetworkError")] NetworkError = 3037,

    /// <summary>
    /// 数据已经存在
    /// </summary>
    [Display(Name = "数据已经存在")]
    [EnumMember(Value = "DataAlreadyExists")] DataAlreadyExists = 3038,

    /// <summary>
    /// 无效操作
    /// </summary>
    [Display(Name = "无效操作")]
    [EnumMember(Value = "InvalidOperation")] InvalidOperation = 3039,

    /// <summary>
    /// 已经达到上限
    /// </summary>
    [Display(Name = "已经达到上限")]
    [EnumMember(Value = "HasReachedUpperLimit")] HasReachedUpperLimit = 3040,

    /// <summary>
    /// 用户不存在
    /// </summary>
    [Display(Name = "用户不存在")]
    [EnumMember(Value = "UserNotExists")] UserNotExists = 3041,

    /// <summary>
    /// 商户不存在
    /// </summary>
    [Display(Name = "商户不存在")]
    [EnumMember(Value = "MerchantNotExists")] MerchantNotExists = 3042,

    /// <summary>
    /// 店铺不存在
    /// </summary>
    [Display(Name = "店铺不存在")]
    [EnumMember(Value = "StoreNotExists")] StoreNotExists = 3043,

    /// <summary>
    /// 材料池异常
    /// </summary>
    [Display(Name = "材料池异常")]
    [EnumMember(Value = "MaterialPoolAbnormal")] MaterialPoolAbnormal = 3044,

    /// <summary>
    /// 材料池状态异常
    /// </summary>
    [Display(Name = "材料池状态异常")]
    [EnumMember(Value = "MaterialPoolStatusAbnormal")] MaterialPoolStatusAbnormal = 3045,

    /// <summary>
    /// 尝试访问不存在的键
    /// </summary>
    [Display(Name = "尝试访问不存在的键")]
    [EnumMember(Value = "KeyNotFound")] KeyNotFound = 3046,

    /// <summary>
    /// 未经授权的资源
    /// </summary>
    [Display(Name = "未经授权的资源")]
    [EnumMember(Value = "UnauthorizedAccess")] UnauthorizedAccess = 3047,

    /// <summary>
    /// DaprApi 操作异常
    /// </summary>
    [Display(Name = "DaprApi 操作异常")]
    [EnumMember(Value = "DaprApi")] DaprApi = 3048,

    /// <summary>
    /// DaprActor 操作异常
    /// </summary>
    [Display(Name = "DaprActor 操作异常")]
    [EnumMember(Value = "ActorInvoke")] ActorInvoke = 3049,
}

#endregion

#region 错误编码扩展类

/// <summary>
/// 错误编码扩展类
/// </summary>
public static class ErrorCodeExtensions
{
    /// <summary>
    /// 是否正常
    /// </summary>
    /// <param name="errorCode"></param>
    /// <returns></returns>
    [Obsolete]
    public static bool IsSuccessful(this ErrorCode errorCode)
    {
        return errorCode == ErrorCode.Success || errorCode == 0;
    }
}

#endregion
