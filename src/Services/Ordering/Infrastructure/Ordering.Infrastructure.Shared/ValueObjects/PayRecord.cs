namespace Ordering.Infrastructure.Shared.ValueObjects;

public record PayRecord
{
    /// <summary>
    ///     支付记录ID
    /// </summary>
    public string Id { get; init; } = string.Empty;

    /// <summary>
    ///     业务费用类型
    /// </summary>
    public int ExpenseType { get; init; }

    /// <summary>
    ///     收付类型
    /// </summary>
    /// <remarks>1: 付款; 2: 收款; 3: 转账; 4: 退款;</remarks>
    public int PayMode { get; init; }

    /// <summary>
    ///     金额
    /// </summary>
    public decimal Amount { get; init; }

    /// <summary>
    ///     业务描述
    /// </summary>
    public string? Remark { get; init; }
}