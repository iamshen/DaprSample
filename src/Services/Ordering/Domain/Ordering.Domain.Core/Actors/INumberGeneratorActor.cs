using Dapr.Actors;

namespace Ordering.Domain.Core.Actors;

/// <summary>
/// </summary>
public interface INumberGeneratorActor : IActor
{
    /// <summary>
    ///     生成序列号
    /// </summary>
    /// <param name="orderType">订单类型, 最多 三位数</param>
    /// <param name="bizType">业务类型, 最多 两位数</param>
    /// <returns></returns>
    Task<string> GenerateNumberAsync(int orderType, int bizType);
}