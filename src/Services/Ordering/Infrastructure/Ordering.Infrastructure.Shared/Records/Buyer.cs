namespace Ordering.Infrastructure.Shared.Records;

/// <summary>
///     购买者
/// </summary>
public record Buyer
{
    /// <summary>
    ///     购买者Id
    /// </summary>
    public string BuyerId { get; set; } = string.Empty;

    /// <summary>
    ///     购买者名称
    /// </summary>
    public string BuyerName { get; set; } = string.Empty;
}