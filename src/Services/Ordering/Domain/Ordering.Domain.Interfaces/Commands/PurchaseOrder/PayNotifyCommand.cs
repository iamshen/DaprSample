using DaprTool.BuildingBlocks.Domain.Attributes;
using DaprTool.BuildingBlocks.Domain.Command;
using Ordering.Infrastructure.Shared.Records;

namespace Ordering.Domain.Interfaces.Commands.PurchaseOrder;

/// <summary>
///     支付通知命令
/// </summary>
[Command(Name = nameof(PayNotifyCommand))]
public class PayNotifyCommand : IntegrationCommand
{
    /// <summary>
    ///     支付记录
    /// </summary>
    public PayRecord PayRecord { get; init; } = new();

    /// <summary>
    ///     支付是否成功
    /// </summary>
    public bool IsSuccess { get; init; }
}