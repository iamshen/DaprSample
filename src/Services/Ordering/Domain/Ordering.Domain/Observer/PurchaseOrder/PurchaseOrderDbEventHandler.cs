using System.Globalization;
using DaprTool.BuildingBlocks.Domain.Logging;
using LinqToDB;
using LinqToDB.Data;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Interfaces.Events.PurchaseOrder;
using Ordering.Domain.Interfaces.Observer.PurchaseOrder;
using Ordering.Infrastructure.Repository.Entities;
using Ordering.Infrastructure.Shared.Enumerations.PurchaseOrder;

namespace Ordering.Domain.Observer.PurchaseOrder;

/// <summary>
///     买料订单 db 事件处理器
/// </summary>
public class PurchaseOrderDbEventHandler(IServiceProvider serviceProvider, ILogger<PurchaseOrderDbEventHandler> logger)
    : IPurchaseOrderDbEventHandler
{
    public async Task Handle(OrderSubmittedEvent @event, CancellationToken cancellationToken)
    {
        try
        {
            await using var db = serviceProvider.GetAppConnection();

            await db.InsertAsync(new PurchaseOrderTable
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

            await db.BulkCopyAsync(@event.OrderItems.Select(x => new PurchaseOrderItemTable
            {
                Id = Guid.NewGuid().ToString(),
                ProductId = x.ProductId,
                ProductName = x.ProductName,
                UnitPrice = x.UnitPrice,
                LaborCost = x.LaborCost,
                Pictures = x.Pictures.ToList()
            }), cancellationToken: cancellationToken);

            logger.LogDbEventSuccess(nameof(OrderSubmittedEvent), @event.EventId);
        }
        catch (Exception ex)
        {
            logger.LogDbEventFailure(nameof(OrderSubmittedEvent), @event.EventId, ex);
        }
    }

    public async Task Handle(OrderSubmittedToPayEvent @event, CancellationToken cancellationToken)
    {
        var type = @event.GetType();
        var value = string.Format(CultureInfo.InvariantCulture, "{0} Notification received: {1}",
            nameof(PurchaseOrderDbEventHandler), type.FullName);
        Console.WriteLine(value);
        await Task.Delay(0, cancellationToken);

        await Task.CompletedTask;
    }

    public async Task Handle(OrderStatusChangeToCancelEvent @event, CancellationToken cancellationToken)
    {
        try
        {
            await using var db = serviceProvider.GetAppConnection();

            await db.PurchaseOrder.Where(x => x.Id == @event.ActorId && x.OrderNo == @event.OrderNo)
                .Set(x => x.OrderStatus, OrderStatus.Cancelled)
                .Set(x => x.UpdatedTime, TimeProvider.System.GetTimestamp())
                .UpdateAsync(cancellationToken);

            logger.LogDbEventSuccess(nameof(OrderStatusChangeToCancelEvent), @event.EventId);
        }
        catch (Exception ex)
        {
            logger.LogDbEventFailure(nameof(OrderStatusChangeToCancelEvent), @event.EventId, ex);
        }
    }
}