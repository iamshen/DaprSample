using System.ComponentModel.DataAnnotations;

namespace Ordering.Infrastructure.Shared.Enumerations.SaleOrder;

/// <summary>
///     销售订单支付状态
/// </summary>
public enum PayStatus : byte
{
    /// <summary>
    ///     已创建 （待支付）
    /// </summary>
    [Display(Name = "已创建")] Created = 1,

    /// <summary>
    ///     支付中
    /// </summary>
    [Display(Name = "支付中")] Paying,

    /// <summary>
    ///     已支付
    /// </summary>
    [Display(Name = "已支付")] Paid,

    /// <summary>
    ///     已失败
    /// </summary>
    [Display(Name = "已失败")] Failed,

    /// <summary>
    ///     已取消
    /// </summary>
    [Display(Name = "已取消")] Cancelled
}
