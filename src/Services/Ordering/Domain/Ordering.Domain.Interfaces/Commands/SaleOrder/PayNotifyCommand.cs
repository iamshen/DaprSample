using Ordering.Infrastructure.Shared.ValueObjects;

namespace Ordering.Domain.Interfaces.Commands.SaleOrder;

public class PayNotifyCommand
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