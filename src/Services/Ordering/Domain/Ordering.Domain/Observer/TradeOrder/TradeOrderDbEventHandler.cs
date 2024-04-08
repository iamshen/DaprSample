using System.Globalization;
using DaprTool.BuildingBlocks.Abstractions.Logging;
using LinqToDB;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Events.TradeOrder;
using Ordering.Infrastructure.Repository.Entities;
using Ordering.Infrastructure.Shared.Enumerations.TradeOrder;

namespace Ordering.Domain.Observer.TradeOrder;

/// <summary>
///     买卖料订单 db 事件处理器
/// </summary>
public class TradeOrderDbEventHandler(IServiceProvider serviceProvider, ILogger<TradeOrderDbEventHandler> logger)
    : ITradeOrderDbEventHandler
{
    public async Task Handle(OrderSubmittedEvent @event, CancellationToken cancellationToken)
    {
        try
        {
            await using var db = serviceProvider.GetAppConnection();

            await db.InsertAsync(new TradeOrderTable
            {
                Id = @event.ActorId,
                OrderNo = @event.OrderNo,
                OrderStatus = @event.OrderStatus,
                PayStatus = @event.PayStatus,
                TradeStatus = @event.TradeStatus,
                Buyer = @event.Buyer,
                Seller = @event.Seller,
                SaleStore = @event.SaleStore,
                Remark = @event.Remark
            }, token: cancellationToken);

            foreach (var item in @event.OrderItems)
                await db.InsertAsync(new TradeOrderItemTable
                {
                    Id = Guid.NewGuid().ToString(),
                    ProductId = item.ProductId,
                    ProductName = item.ProductName,
                    UnitPrice = item.UnitPrice,
                    LaborCost = item.LaborCost,
                    Pictures = item.Pictures.ToList()
                }, token: cancellationToken);

            logger.LogDbEventSuccess(nameof(OrderSubmittedEvent), @event.EventId);
        }
        catch (Exception ex)
        {
            logger.LogDbEventFailure(nameof(OrderSubmittedEvent), @event.EventId, ex.Message, ex);
        }
    }

    public async Task Handle(OrderSubmittedToPayEvent @event, CancellationToken cancellationToken)
    {
        var type = @event.GetType();
        var value = string.Format(CultureInfo.InvariantCulture, "{0} Notification received: {1}",
            nameof(TradeOrderDbEventHandler), type.FullName);
        Console.WriteLine(value);
        await Task.Delay(0, cancellationToken);

        await Task.CompletedTask;
    }

    public async Task Handle(OrderStatusChangeToCancelEvent @event, CancellationToken cancellationToken)
    {
        try
        {
            await using var db = serviceProvider.GetAppConnection();

            await db.TradeOrder.Where(x => x.Id == @event.ActorId && x.OrderNo == @event.OrderNo)
                .Set(x => x.OrderStatus, OrderStatus.Cancelled)
                .UpdateAsync(cancellationToken);

            logger.LogDbEventSuccess(nameof(OrderStatusChangeToCancelEvent), @event.EventId);
        }
        catch (Exception ex)
        {
            logger.LogDbEventFailure(nameof(OrderStatusChangeToCancelEvent), @event.EventId, ex.Message, ex);
        }
    }
}

public interface ITradeOrderDbEventHandler :
    INotificationHandler<OrderSubmittedEvent>,
    INotificationHandler<OrderSubmittedToPayEvent>,
    INotificationHandler<OrderStatusChangeToCancelEvent>;