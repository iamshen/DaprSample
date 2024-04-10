using Dapr.Actors.Runtime;
using SampleInterfaces;

namespace SampleService;

public class SampleActor(ActorHost host, BankService bankService) : Actor(host), ISampleActor, IBankActor, IRemindable
{
    private const string StateName = nameof(SampleActor);
    private readonly BankService _bankService = bankService;


    public Task<AccountBalance> GetAccountBalance()
    {
        throw new NotImplementedException();
    }

    public async Task SaveData(SampleState state, TimeSpan ttl)
    {

        Console.WriteLine($"This is Actor id {this.Id} with data {state}.");

        await this.StateManager.SetStateAsync<SampleState>(StateName, state, ttl);
    }
    public Task<SampleState> GetData()
    {
        // Get state using StateManager.
        return this.StateManager.GetStateAsync<SampleState>(StateName);
    }
    public Task<ActorReminderData> GetReminder()
    {
        throw new NotImplementedException();
    }

    public Task ReceiveReminderAsync(string reminderName, byte[] state, TimeSpan dueTime, TimeSpan period)
    {
        throw new NotImplementedException();
    }

    public Task RegisterReminder()
    {
        throw new NotImplementedException();
    }

    public Task RegisterReminderWithRepetitions(int repetitions)
    {
        throw new NotImplementedException();
    }

    public Task RegisterReminderWithTtl(TimeSpan ttl)
    {
        throw new NotImplementedException();
    }

    public Task RegisterReminderWithTtlAndRepetitions(TimeSpan ttl, int repetitions)
    {
        throw new NotImplementedException();
    }

    public Task RegisterTimer()
    {
        throw new NotImplementedException();
    }

    public Task RegisterTimerWithTtl(TimeSpan ttl)
    {
        throw new NotImplementedException();
    }


    public Task TestNoArgumentNoReturnType()
    {
        throw new NotImplementedException();
    }

    public Task TestThrowException()
    {
        throw new NotImplementedException();
    }

    public Task UnregisterReminder()
    {
        throw new NotImplementedException();
    }

    public Task UnregisterTimer()
    {
        throw new NotImplementedException();
    }

    public Task Withdraw(WithdrawRequest withdraw)
    {
        throw new NotImplementedException();
    }
}
