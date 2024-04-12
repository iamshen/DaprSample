using Dapr.Actors.Runtime;
using SampleInterfaces;

namespace SampleService;

public class SampleActor : Actor, ISampleActor, IBankActor, IRemindable
{
    private const string StateName = nameof(SampleActor);
    // private readonly IBankService _bankService = bankService;
    // The constructor must accept ActorHost as a parameter, and can also accept additional
    // parameters that will be retrieved from the dependency injection container
    //
    /// <summary>
    /// Initializes a new instance of SmokeDetectorActor
    /// </summary>
    /// <param name="host">The Dapr.Actors.Runtime.ActorHost that will host this actor instance.</param>
    public SampleActor(ActorHost host) : base(host)
    {

    }

    /// <summary>
    /// This method is called whenever an actor is activated.
    /// An actor is activated the first time any of its methods are invoked.
    /// </summary>
    protected override Task OnActivateAsync()
    {
        // Provides opportunity to perform some optional setup.
        Console.WriteLine($"Activating actor id: {this.Id}");
        return Task.CompletedTask;
    }

    /// <summary>
    /// This method is called whenever an actor is deactivated after a period of inactivity.
    /// </summary>
    protected override Task OnDeactivateAsync()
    {
        // Provides opportunity to perform optional cleanup.
        Console.WriteLine($"Deactivating actor id: {Id}");
        return Task.CompletedTask;
    }

    public Task<AccountBalance> GetAccountBalance()
    {
        return Task.FromResult(new AccountBalance { });
    }

    public async Task SaveData(SampleState state, TimeSpan ttl)
    {

        Console.WriteLine($"This is Actor id {this.Id} with data {state}.");

        await this.StateManager.SetStateAsync<SampleState>(StateName, state);
    }
    public Task<SampleState> GetData()
    {
        // Get state using StateManager.
        return this.StateManager.GetStateAsync<SampleState>(StateName);
    }
    public async Task<ActorReminderData?> GetReminder()
    {
        var reminder = await this.GetReminderAsync("TestReminder");

        return reminder is not null
            ? new ActorReminderData { Name = reminder.Name, Period = reminder.Period, DueTime = reminder.DueTime }
            : null;
    }

    public Task ReceiveReminderAsync(string reminderName, byte[] state, TimeSpan dueTime, TimeSpan period)
    {
        return Task.CompletedTask;
    }

    public Task RegisterReminder()
    {
        return Task.CompletedTask;
    }

    public Task RegisterReminderWithRepetitions(int repetitions)
    {
        return Task.CompletedTask;
    }

    public Task RegisterReminderWithTtl(TimeSpan ttl)
    {
        return Task.CompletedTask;
    }

    public Task RegisterReminderWithTtlAndRepetitions(TimeSpan ttl, int repetitions)
    {
        return Task.CompletedTask;
    }

    public Task RegisterTimer()
    {
        return Task.CompletedTask;
    }

    public Task RegisterTimerWithTtl(TimeSpan ttl)
    {
        return Task.CompletedTask;
    }


    public Task TestNoArgumentNoReturnType()
    {
        return Task.CompletedTask;
    }

    public Task TestThrowException()
    {
        return Task.CompletedTask;
    }

    public Task UnregisterReminder()
    {
        return Task.CompletedTask;
    }

    public Task UnregisterTimer()
    {
        return Task.CompletedTask;
    }

    public Task Withdraw(WithdrawRequest withdraw)
    {
        return Task.CompletedTask;
    }
}
