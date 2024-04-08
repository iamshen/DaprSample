namespace Ordering.Infrastructure.Shared.Records;

/// <summary>
///     订单明细
/// </summary>
public record TradeItem
{
    /// <summary>
    ///     产品Id
    /// </summary>
    public string ProductId { get; set; } = string.Empty;

    /// <summary>
    ///     产品名称
    /// </summary>
    public string ProductName { get; set; } = string.Empty;

    /// <summary>
    ///     产品单价
    /// </summary>
    /// <remarks>产品单价：元/克 </remarks>
    public decimal UnitPrice { get; set; }

    /// <summary>
    ///     工费
    /// </summary>
    /// <remarks>产品工费：元/克 </remarks>
    public double LaborCost { get; set; }

    /// <summary>
    ///     产品照片
    /// </summary>
    public IList<ReferResources> Pictures { get; set; } = new List<ReferResources>();
}