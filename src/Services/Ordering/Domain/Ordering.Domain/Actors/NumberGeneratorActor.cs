using Dapr.Actors.Runtime;
using Ordering.Domain.Interfaces.Actors;

namespace Ordering.Domain.Actors;

/// <summary>
/// </summary>
public class NumberGeneratorActor : Actor, INumberGeneratorActor
{
    private const string StateDataKey = "number-generator-key";

    /// <summary>
    ///     ctor
    /// </summary>
    /// <param name="host"></param>
    public NumberGeneratorActor(ActorHost host) : base(host)
    {
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<string> GenerateNumberAsync(int bizType)
    {
        // 流水号计数器
        long counter = 0;
        var numberState = await StateManager.TryGetStateAsync<long>(StateDataKey);
        if (numberState.HasValue) counter = numberState.Value;

        var datePart = DateTime.UtcNow.ToString("yyyyMMdd"); // 当前日期部分

        var orderNumber = $"{bizType}{datePart}{counter:D7}";

        counter++; // 递增流水号

        await StateManager.SetStateAsync(StateDataKey, counter);

        return orderNumber;
    }
}