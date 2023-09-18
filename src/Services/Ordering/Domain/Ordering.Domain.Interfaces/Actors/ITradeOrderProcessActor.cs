﻿using Dapr.Actors;
using Ordering.Domain.Interfaces.Commands.TradeOrder;
using Ordering.Infrastructure.Shared.Dtos.TradeOrder;
using Ordering.Infrastructure.Shared.ValueObjects;

namespace Ordering.Domain.Interfaces.Actors;

public interface ITradeOrderProcessActor : IActor
{
    /// <summary>
    ///     提交订单
    /// </summary>
    /// <param name="orderCommand"></param>
    /// <returns></returns>
    public Task<OrderRecord> SubmitAsync(CreateTradeOrderCommand orderCommand);

    /// <summary>
    ///     获取订单详情
    /// </summary>
    /// <returns></returns>
    public Task<TradeOrderOutputDto> GetAsync();
}