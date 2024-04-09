using Dapr.Actors;
using Dapr.Actors.Client;
using LanguageExt.Common;
using LinqToDB;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ordering.Application.Interfaces;
using Ordering.Domain.Interfaces.Actors;
using Ordering.Domain.Interfaces.Commands.PurchaseOrder;
using Ordering.Infrastructure.Shared.Dtos.PurchaseOrder;
using Ordering.Infrastructure.Shared.Records;

namespace Ordering.Application.Services;

/// <summary>
///     买料订单服务
/// </summary>
public class PurchaseOrderApiService : IPurchaseOrderApiService
{
    private const string ActorType = "PurchaseOrderProcessActor";
    private readonly ILogger<PurchaseOrderApiService> _logger;
    private readonly IServiceProvider _serviceProvider;

    /// <summary>
    ///     ctor
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="serviceProvider"></param>
    public PurchaseOrderApiService(ILogger<PurchaseOrderApiService> logger, IServiceProvider serviceProvider)
    {
        _logger = logger;
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    ///     提交订单
    /// </summary>
    /// <param name="orderCommand"></param>
    /// <returns></returns>
    public async Task<Result<OrderRecord>> SubmitAsync(CreateOrderCommand orderCommand)
    {
        try
        {
            var actor = ActorProxy.Create<IPurchaseOrderProcessActor>(ActorId.CreateRandom(), ActorType);

            var submitResult = await actor.SubmitAsync(orderCommand);
            _logger.LogInformation(submitResult.ToString());
            return new Result<OrderRecord>(value: submitResult);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "提交订单发生异常: {Message}", ex.Message);
            throw;
        }
    }

    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Result<PurchaseOrderOutputDto>> GetAsync(string id)
    {
        return await GetOrderStateAsync(id);
    }

    /// <summary>
    /// </summary>
    /// <param name="orderNumber"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Result<PurchaseOrderOutputDto>> GetByOrderNumberAsync(string orderNumber)
    {
        await using var db = _serviceProvider.GetAppConnection();
        var order = await db.PurchaseOrder.FirstOrDefaultAsync(x => x.OrderNo == orderNumber);
        if (order is null)
        {
            var ex = new Exception($"找不到指定单号：{orderNumber}");
            return new Result<PurchaseOrderOutputDto>(ex);
        }

        return await GetOrderStateAsync(order.Id);
    }

    /// <summary>
    ///     获取订单State
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    private async Task<Result<PurchaseOrderOutputDto>> GetOrderStateAsync(string id)
    {
        try
        {
            var proxy = ActorProxy.DefaultProxyFactory.Create(new ActorId(id), ActorType);
            var result = await proxy.InvokeMethodAsync<PurchaseOrderOutputDto>("GetAsync");
            return new Result<PurchaseOrderOutputDto>(result);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "获取订单发生异常: {Message}", ex.Message);
            return new Result<PurchaseOrderOutputDto>(ex);
        }
    }
}