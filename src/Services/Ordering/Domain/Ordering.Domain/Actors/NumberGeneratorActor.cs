using Dapr.Actors.Runtime;
using Ordering.Domain.Core.Actors;

namespace Ordering.Domain.Actors;

/// <summary>
///     流水号生成器
/// </summary>
public class NumberGeneratorActor : Actor, INumberGeneratorActor
{
    private const string StateDataKey = "serial-number-{0}-{1}-{2}";

    /// <summary>
    ///     ctor
    /// </summary>
    /// <param name="host"></param>
    public NumberGeneratorActor(ActorHost host) : base(host)
    {
    }

    /// <summary>
    ///     生成序列号
    /// </summary>
    /// <param name="orderType">订单类型, 最多 三位数</param>
    /// <param name="bizType">业务类型, 最多 两位数</param>
    /// <returns>单号</returns>
    /// <remarks>
    ///     <para>单号 = 订单类型 + 业务类型 + 日期 + 计数器</para>
    ///     <para>例如: "201122023091800000001" </para>
    /// </remarks>
    /// >
    public async Task<string> GenerateNumberAsync(int orderType, int bizType)
    {
        // 流水号计数器
        long counter = 0;

        var orderTypeStr = $"{orderType:D3}";
        var bizTypeStr = $"{bizType:D2}";
        var dateTimeStr = $"{DateTime.UtcNow:yyyyMMdd}";

        var key = string.Format(StateDataKey, dateTimeStr, orderTypeStr, bizTypeStr);
        var numberState = await StateManager.TryGetStateAsync<long>(key);
        if (numberState.HasValue) counter = numberState.Value;

        // 单号 = 订单类型 + 业务类型 + 日期 + 计数器
        var orderNumber = $"{orderTypeStr}{bizTypeStr}{dateTimeStr}{counter:D8}";
        counter++; // 递增流水号
        await StateManager.SetStateAsync(key, counter);
        return orderNumber;
    }
}