using Common.Domain.Interfaces.Actors;
using Dapr.Actors.Runtime;

namespace Common.Domain.Actors;

/// <summary>
/// 
/// </summary>
public class NumberGeneratorActor : Actor, INumberGeneratorActor
{
    private const string StateDataKey = "number-generator-key";

    // 每次递增数量
    private const int Quantity = 1;

    // 
    private const int TotalWith = 12;

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

        var orderNumber = $"{bizType}{datePart}{counter:D8}";

        counter++; // 递增流水号

        await StateManager.SetStateAsync(StateDataKey, counter);

        return orderNumber;
    }
}