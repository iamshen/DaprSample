using SampleInterfaces;

namespace SampleService;

public class BankService : IBankService
{
    // 信用额度，允许透支50块。
    private readonly decimal OverdraftThreshold = -50m;

    public decimal Withdraw(decimal balance, decimal amount)
    {
        // 除了基本的审计逻辑之外，还可以在这里做一些复杂的审计核算逻辑

        var updated = balance - amount;
        if (updated < OverdraftThreshold)
        {
            throw new OverdraftException(balance, amount);
        }

        return updated;
    }
}
