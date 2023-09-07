using Dapr.Actors;
using Dapr.Actors.Client;
using LanguageExt.Common;
using Ordering.Application.Interfaces;
using Ordering.Domain.Interfaces.Actors;
using Ordering.Domain.Interfaces.Commands.TradeOrder;
using Ordering.Infrastructure.Shared.Dtos.TradeOrder;
using Ordering.Infrastructure.Shared.ValueObjects;

namespace Ordering.Application.Services;

/// <summary>
///     买卖料订单服务
/// </summary>
public class TradeOrderService : ITradeOrderService
{
    private const string ActorType = "OrderingProcessActorActor";

    /// <summary>
    /// </summary>
    /// <param name="orderCommand"></param>
    /// <returns></returns>
    public async Task<Result<IdNumberRecord>> SubmitAsync(CreateTradeOrderCommand orderCommand)
    {
        var proxyActor = ActorProxy.Create<ITradeOrderProcessActor>(ActorId.CreateRandom(), ActorType);

        var submitResult = await proxyActor.SubmitAsync(orderCommand);

        return submitResult;
    }


    /// <summary>
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    public async Task<Result<TradeOrderOutputDto>> GetAsync(string id)
    {
        var proxyActor = ActorProxy.Create<ITradeOrderProcessActor>(new ActorId(id), ActorType);

        var result = await proxyActor.GetAsync();

        return result;
    }

    /// <summary>
    /// </summary>
    /// <param name="orderNumber"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<Result<TradeOrderOutputDto>> GetByOrderNumberAsync(string orderNumber)
    {
        // TODO:  get db connection query the order by orderNumber
        await Task.CompletedTask;
        throw new NotImplementedException();
    }
}