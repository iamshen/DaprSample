using Dapr.Actors;
using DaprTool.BuildingBlocks.Utils.ValueObjects;
using Ordering.Domain.Interfaces.Commands.PurchaseOrder;
using Ordering.Infrastructure.Shared.Dtos.PurchaseOrder;
using Ordering.Infrastructure.Shared.Records;

namespace Ordering.Domain.Interfaces.Actors;

/// <summary>
///     买料订单
/// </summary>
public interface IPurchaseOrderProcessActor : IActor
{
    /// <summary>
    ///     提交订单
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public Task<OrderRecord> SubmitAsync(CreateOrderCommand command);

    /// <summary>
    ///     获取订单详情
    /// </summary>
    /// <returns></returns>
    public Task<PurchaseOrderOutputDto> GetAsync();
}