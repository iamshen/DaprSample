namespace Ordering.Infrastructure.Shared.ValueObjects;

/// <summary>
///     销售人员
/// </summary>
public record Seller
{
    /// <summary>
    ///     销售者Id
    /// </summary>
    /// <remarks>销售员工的Id</remarks>
    public string SellerId { get; set; } = string.Empty;

    /// <summary>
    ///     销售者名称
    /// </summary>
    /// <remarks>销售员工的名称</remarks>
    public string SellerName { get; set; } = string.Empty;
}