using DaprTool.BuildingBlocks.Domain.Attributes;
using DaprTool.BuildingBlocks.Domain.Command;
using Ordering.Infrastructure.Shared.Records;

namespace Ordering.Domain.Interfaces.Commands.PurchaseOrder;

/// <summary>
///     创建买料订单
/// </summary>
[Command(Name = nameof(CreateOrderCommand))]
public class CreateOrderCommand : IntegrationCommand
{
    /// <summary>
    ///     购买人
    /// </summary>
    public Buyer Buyer { get; set; } = new();

    /// <summary>
    ///     销售员工
    /// </summary>
    public Seller Seller { get; set; } = new();

    /// <summary>
    ///     销售门店
    /// </summary>
    public SaleStore SaleStore { get; set; } = new();

    /// <summary>
    ///     备注
    /// </summary>
    public string? Remark { get; set; }

    /// <summary>
    ///     订单明细
    /// </summary>
    public IList<TradeItem> OrderItems { get; set; } = new List<TradeItem>();
}