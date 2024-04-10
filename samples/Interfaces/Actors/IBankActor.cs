using Dapr.Actors;


namespace SampleInterfaces;

public interface IBankActor : IActor
{
    Task<AccountBalance> GetAccountBalance();

    Task Withdraw(WithdrawRequest withdraw);
}
