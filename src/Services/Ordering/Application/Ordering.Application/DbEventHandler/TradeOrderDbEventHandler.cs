using DaprTool.BuildingBlocks.CommonUtility;
using DaprTool.BuildingBlocks.EventBus.Abstractions;
using LinqToDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ordering.Domain.Interfaces.Events.TradeOrder;
using Ordering.Infrastructure.Linq2Db.Entities;

namespace Ordering.Application.DbEventHandler;

/// <summary>
///     买卖料订单 db 事件处理器
/// </summary>
public class TradeOrderDbEventHandler : IDbEventHandler
{
    private readonly ILogger<TradeOrderDbEventHandler> _logger;
    private readonly IServiceProvider _serviceProvider;

    public TradeOrderDbEventHandler(IServiceProvider serviceProvider, ILogger<TradeOrderDbEventHandler> logger)
    {
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    public async Task Handle(OrderSubmittedDbEvent @event)
    {
        try
        {
            await using var db = _serviceProvider.GetAppConnection();

            await db.InsertAsync(new TradeOrderTable
            {
                Id = @event.ActorId,
                CreatedTime = DateTimeHelper.GetTimestamp(),
                DeletedTime =  0,
                UpdatedTime = DateTimeHelper.GetTimestamp(),
                OrderNo = @event.OrderNo,
                OrderStatus = @event.OrderStatus,
                PayStatus = @event.PayStatus,
                TradeStatus = @event.TradeStatus,
                Buyer = @event.Buyer,
                Seller = @event.Seller,
                SaleStore = @event.SaleStore,
                Remark = @event.Remark
            });
            
            _logger.LogInformation("DbEvent Handler {EventName} Success; EventId:{EventId}", 
                nameof(OrderSubmittedDbEvent), 
                @event.Id);
        }
        catch (Exception ex)
        {
           _logger.LogError(ex, 
               "DbEvent Handler {EventName} Fail; EventId:{EventId}; Message: {Message}", 
               nameof(OrderSubmittedDbEvent), 
               @event.Id,
               ex.Message);
        }
    }
}