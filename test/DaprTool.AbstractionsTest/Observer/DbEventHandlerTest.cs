using System.Globalization;
using DaprTool.AbstractionsTest.Base;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Xunit.Abstractions;

namespace DaprTool.AbstractionsTest;

/// <summary>
///     ctor
/// </summary>
/// <param name="fixture"></param>
/// <param name="testOutputHelper"></param>
public class DbEventHandlerTest(ObserverDependencySetupFixture fixture, ITestOutputHelper testOutputHelper)
    : IClassFixture<ObserverDependencySetupFixture>
{
    private readonly IServiceProvider _serviceProvider = fixture.ServiceProvider;

    [Fact(DisplayName = "01. 测试事件进程间发布订阅")]
    public async Task Test1()
    {
        using var scope = _serviceProvider.CreateScope();
        var mediator = scope.ServiceProvider.GetRequiredService<IMediator>();

        var order = new OrderSubmittedEvent { ProductName = "iphone" };
        testOutputHelper.WriteLine(order.ToString());

        await mediator.Publish(order);

        var evt1 = new OrderSubmittedToPayEvent { Amount = order.Amount };
        await mediator.Publish(evt1);
        testOutputHelper.WriteLine(evt1.ToString());

        var evt2 = new OrderStatusChangeToCancelEvent { OrderStatus = 1 };
        await mediator.Publish(evt2);
        testOutputHelper.WriteLine(evt2.ToString());
    }

    public interface IOrderProcessDbEventHandler : INotificationHandler<OrderSubmittedEvent>,
        INotificationHandler<OrderSubmittedToPayEvent>,
        INotificationHandler<OrderStatusChangeToCancelEvent>;

    public class OrderProcessDbEventHandler : IOrderProcessDbEventHandler
    {
        public Task Handle(OrderSubmittedEvent @event, CancellationToken cancellationToken)
        {
            var type = @event.GetType();
            var value = string.Format(CultureInfo.InvariantCulture, "{0} Notification received: {1}",
                nameof(OrderProcessDbEventHandler), type.FullName);

            Console.WriteLine(value);
            return Task.CompletedTask;
        }

        public Task Handle(OrderSubmittedToPayEvent @event, CancellationToken cancellationToken)
        {
            var type = @event.GetType();
            var value = string.Format(CultureInfo.InvariantCulture, "{0} Notification received: {1}",
                nameof(OrderProcessDbEventHandler), type.FullName);

            Console.WriteLine(value);
            return Task.CompletedTask;
        }

        public Task Handle(OrderStatusChangeToCancelEvent @event, CancellationToken cancellationToken)
        {
            var type = @event.GetType();
            var value = string.Format(CultureInfo.InvariantCulture, "{0} Notification received: {1}",
                nameof(OrderProcessDbEventHandler), type.FullName);

            Console.WriteLine(value);
            return Task.CompletedTask;
        }
    }

    public interface IEvent : INotification
    {
    }

    public record OrderSubmittedEvent : IEvent
    {
        public string OrderNo { get; set; } = Guid.NewGuid().ToString("N");
        public string CustomerId { get; set; } = Guid.NewGuid().ToString("N");
        public required string ProductName { get; init; }
        public double Amount { get; set; } = Random.Shared.NextDouble();
        public int OrderStatus { get; init; } = 0;
    }

    public record OrderSubmittedToPayEvent : IEvent
    {
        public required double Amount { get; init; }
    }

    public record OrderStatusChangeToCancelEvent : IEvent
    {
        public required int OrderStatus { get; init; }
    }
}