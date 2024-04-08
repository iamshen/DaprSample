using DaprTool.BuildingBlocks.Abstractions.Attributes;
using DaprTool.BuildingBlocks.Abstractions.Command;
using Ordering.Infrastructure.Shared.Records;

namespace Ordering.Domain.Commands.TradeOrder;

/// <summary>
///     支付通知命令
/// </summary>
[Command(Name = "PayNotify")]
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