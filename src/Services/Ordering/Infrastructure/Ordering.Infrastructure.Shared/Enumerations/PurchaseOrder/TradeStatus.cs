using System.ComponentModel.DataAnnotations;

namespace Ordering.Infrastructure.Shared.Enumerations.PurchaseOrder;

/// <summary>
///     交易状态
/// </summary>
public enum TradeStatus : byte
{
    /// <summary>
    ///     待交易
    /// </summary>
    [Display(Name = "待交易")] Created = 1,

    /// <summary>
    ///     交易中
    /// </summary>
    [Display(Name = "交易中")] Trading,

    /// <summary>
    ///     已完成
    /// </summary>
    [Display(Name = "已完成")] Completed,

    /// <summary>
    ///     已失败
    /// </summary>
    [Display(Name = "已失败")] Fail,

    /// <summary>
    ///     已取消
    /// </summary>
    [Display(Name = "已取消")] Cancel
}
