namespace Ordering.Infrastructure.Shared.ValueObjects;

/// <summary>
///     销售柜台
/// </summary>
public record SaleStore
{
    /// <summary>
    ///     销售柜台Id
    /// </summary>
    /// <remarks>销售 店铺/柜台 Id</remarks>
    public string SellerId { get; set; } = string.Empty;

    /// <summary>
    ///     销售柜台名称
    /// </summary>
    /// <remarks>销售 店铺/柜台 名称</remarks>
    public string SellerName { get; set; } = string.Empty;
}