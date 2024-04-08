using Dapr.Client;
using DaprTool.BuildingBlocks.Abstractions.Events;
using DaprTool.BuildingBlocks.Utils.Constant;
using Microsoft.Extensions.Logging;

namespace DaprTool.BuildingBlocks.Abstractions.EventBus;

/// <summary>
///     Dapr 事件总线
/// </summary>
public class DaprEventBus(DaprClient dapr, ILogger<DaprEventBus> logger) : IEventBus
{
    /// <summary>
    ///     发布事件
    /// </summary>
    /// <param name="events"></param>
    public async Task RaiseAsync(params IntegrationEvent[] events)
    {
        foreach (var integrationEvent in events)
        {
            var topicName = integrationEvent.GetType().Name;

            logger.LogDebug(
                "发布事件 {@Event} 到 {PubsubName}.{TopicName}",
                integrationEvent,
                DaprConstants.PubSubName,
                topicName);

            // 通过将事件转换为动态，
            // 并将具体类型传给 PublishEventAsync
            // 这样能确保所有事件的字段都能正确序列化。
            await dapr.PublishEventAsync(
                DaprConstants.PubSubName,
                topicName,
                (object)integrationEvent);
        }
    }
}