using Dapr.Actors;
using Dapr.Actors.Client;
using LanguageExt.Common;
using LinqToDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ordering.Application.Interfaces;
using Ordering.Domain.Commands.TradeOrder;
using Ordering.Infrastructure.Shared.Dtos.TradeOrder;
using Ordering.Infrastructure.Shared.Records;

namespace Ordering.Application.Services;

/// <summary>
///     买卖料订单服务
/// </summary>
public class TradeOrderService : ITradeOrderService 
{
    private const string ActorType = "TradeOrderProcessActor";
    private readonly ILogger<TradeOrderService> _logger;
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    ///     ctor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="serviceProvider"></param>
    public TradeOrderService(ILogger<TradeOrderService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    ///     提交订单
    /// </summary>
    /// <param name="orderCommand"></param>
    /// <returns></returns>
    public async Task<Result<OrderRecord>> SubmitAsync(CreateTradeOrderCommand orderCommand)
    {
        try
        {
            //var proxy1 = ActorProxy.Create<ITradeOrderProcessActor>(ActorId.CreateRandom(), ActorType);
            //var submitResult1 = await proxy1.SubmitAsync(orderCommand);
            //Console.WriteLine(submitResult1.OrderNo);

            var proxy = ActorProxy.DefaultProxyFactory.Create(ActorId.CreateRandom(), ActorType);
            var submitResult = await proxy.InvokeMethodAsync<CreateTradeOrderCommand, OrderRecord>("SubmitAsync", orderCommand);

            return new Result<OrderRecord>(submitResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "提交订单发生异常: {Message}", ex.Message);
            return new Result<OrderRecord>(ex);
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Result<TradeOrderOutputDto>> GetAsync(string id)
    {
        return await GetOrderStateAsync(id);
    }

    /// <summary>
    /// </summary>
    /// <param name="orderNumber"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Result<TradeOrderOutputDto>> GetByOrderNumberAsync(string orderNumber)
    {
        await using var db = _serviceProvider.GetAppConnection();
        var order = await db.TradeOrder.FirstOrDefaultAsync(x => x.OrderNo == orderNumber);
        if (order is null)
        {
            var ex = new Exception($"找不到指定单号：{orderNumber}");
            return new Result<TradeOrderOutputDto>(ex);
        }

        return await GetOrderStateAsync(order.Id);
    }

    /// <summary>
    ///     获取订单State
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private async Task<Result<TradeOrderOutputDto>> GetOrderStateAsync(string id)
    {
        try
        {
            var proxy = ActorProxy.DefaultProxyFactory.Create(new ActorId(id), ActorType);
            var result = await proxy.InvokeMethodAsync<TradeOrderOutputDto>("GetAsync");
            return new Result<TradeOrderOutputDto>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取订单发生异常: {Message}", ex.Message);
            return new Result<TradeOrderOutputDto>(ex);
        }
    }
}