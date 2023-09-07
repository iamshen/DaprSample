using Dapr.Client;
using DaprTool.BuildingBlocks.EventBus.Abstractions;
using DaprTool.BuildingBlocks.EventBus.Events;
using Microsoft.Extensions.Logging;

namespace DaprTool.BuildingBlocks.EventBus;

/// <summary>
///     Dapr 事件总线
/// </summary>
public class DaprEventBus : IEventBus
{
    private const string PubSubName = "dapr-solution-pubsub";

    private readonly DaprClient _dapr;
    private readonly ILogger _logger;

    public DaprEventBus(DaprClient dapr, ILogger logger)
    {
        _dapr = dapr;
        _logger = logger;
    }

    /// <summary>
    ///     发布事件
    /// </summary>
    /// <param name="integrationEvents"></param>
    public async Task RaiseAsync(params IntegrationEvent[] integrationEvents)
    {
        foreach (var integrationEvent in integrationEvents)
        {
            var topicName = integrationEvent.GetType().Name;

            _logger.LogInformation("发布事件 {@Event} 到 {PubsubName}.{TopicName}",
                integrationEvent,
                PubSubName,
                topicName);

            // 通过将事件转换为动态，并将具体类型传给 PublishEventAsync 这样能确保所有事件的字段都能正确序列化。
            await _dapr.PublishEventAsync(PubSubName, topicName, (object)integrationEvent);
        }
    }
}
