using System.Globalization;
using DaprTool.AbstractionsTest.Base;
using Google.Protobuf.WellKnownTypes;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace DaprTool.AbstractionsTest;

/// <summary>
///     ctor
/// </summary>
/// <param name="fixture"></param>
/// <param name="testOutputHelper"></param>
public class DbEventHandlerTest(DependencySetupFixture fixture, ITestOutputHelper testOutputHelper) : IClassFixture<DependencySetupFixture>
{
    private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;
    private readonly IMessageSink _diagnosticMessageSink = fixture.Sink;

    [Fact(DisplayName = "01. 测试事件进程间发布订阅")]
    public async Task Test1()
    {
        using var scope = _serviceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();
        _diagnosticMessageSink.OnMessage(new DiagnosticMessage("Start publish Events..."));

        var order = new OrderSubmittedEvent() { ProductName = "iphone" };
        testOutputHelper.WriteLine(order.Serialize());

        await mediator.Publish(order);
        _diagnosticMessageSink.OnMessage(new DiagnosticMessage("Order Submitted..."));

        await mediator.Publish(new OrderSubmittedToPayEvent { Amount = order.Amount });
        _diagnosticMessageSink.OnMessage(new DiagnosticMessage("Order wait for Pay..."));

        await mediator.Publish(new OrderStatusChangeToCancelEvent { OrderStatus = 1 });
        _diagnosticMessageSink.OnMessage(new DiagnosticMessage("Order Status Change..."));

    }

    public interface IOrderProcessDbEventHandler : 
        INotificationHandler<OrderSubmittedEvent>, 
        INotificationHandler<OrderSubmittedToPayEvent>, 
        INotificationHandler<OrderStatusChangeToCancelEvent>
    {  }

    public class OrderProcessDbEventHandler : IOrderProcessDbEventHandler
    {
        public Task Handle(OrderSubmittedEvent @event, CancellationToken cancellationToken)
        {
            var type = @event.GetType();
            var value = string.Format(CultureInfo.InvariantCulture, "{0} Notification received: {1}", nameof(OrderProcessDbEventHandler), type.FullName);

            Console.WriteLine(value);
            return Task.CompletedTask;
        }

        public Task Handle(OrderSubmittedToPayEvent @event, CancellationToken cancellationToken)
        {
            var type = @event.GetType();
            var value = string.Format(CultureInfo.InvariantCulture, "{0} Notification received: {1}", nameof(OrderProcessDbEventHandler), type.FullName);

            Console.WriteLine(value);
            return Task.CompletedTask;
        }

        public Task Handle(OrderStatusChangeToCancelEvent @event, CancellationToken cancellationToken)
        {
            var type = @event.GetType();
            var value = string.Format(CultureInfo.InvariantCulture, "{0} Notification received: {1}", nameof(OrderProcessDbEventHandler), type.FullName);

            Console.WriteLine(value);
            return Task.CompletedTask;
        }

    }

    public interface IEvent : INotification
    {  }

    public class OrderSubmittedEvent : IEvent
    {
        public  string OrderNo { get; set; } = Guid.NewGuid().ToString("N");
        public  string CustomerId { get; set; } = Guid.NewGuid().ToString("N");
        public required string ProductName { get; init; }
        public  double Amount { get; set; } = Random.Shared.NextDouble();
        public int OrderStatus { get; init; } = 0;
    }

    public class OrderSubmittedToPayEvent : IEvent
    {
        public required double Amount { get; init; }
    }

    public class OrderStatusChangeToCancelEvent : IEvent
    {
        public required int OrderStatus { get; init; }
    }
}