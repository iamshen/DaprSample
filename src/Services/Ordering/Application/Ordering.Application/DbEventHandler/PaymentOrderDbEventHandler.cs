using DaprTool.BuildingBlocks.EventBus.Abstractions;

namespace Ordering.Application.DbEventHandler;

public class PaymentOrderDbEventHandler : IDbEventHandler
{
    public string HandlerKeyed => nameof(PaymentOrderDbEventHandler);
}