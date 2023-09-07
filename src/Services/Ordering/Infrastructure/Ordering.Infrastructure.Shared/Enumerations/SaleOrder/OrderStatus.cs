using System.ComponentModel.DataAnnotations;

namespace Ordering.Infrastructure.Shared.Enumerations.SaleOrder;

/// <summary>
///     销售订单状态
/// </summary>
public enum OrderStatus : byte
{
    /// <summary>
    ///     已创建 (待提交)
    /// </summary>
    [Display(Name = "已创建")] Created = 1,

    /// <summary>
    ///     已提交
    /// </summary>
    [Display(Name = "已提交")] Submitted,

    /// <summary>
    ///     待付款
    /// </summary>
    [Display(Name = "待付款")] AwaitingPay,

    /// <summary>
    ///     已完成
    /// </summary>
    [Display(Name = "已完成")] Completed,

    /// <summary>
    ///     已取消
    /// </summary>
    [Display(Name = "已取消")] Cancelled,

    /// <summary>
    ///     已退单
    /// </summary>
    [Display(Name = "已退单")] Returned
}
