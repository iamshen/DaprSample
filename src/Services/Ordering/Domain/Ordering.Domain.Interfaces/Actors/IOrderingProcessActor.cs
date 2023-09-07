using LanguageExt.Common;
using Ordering.Domain.Interfaces.Commands.SaleOrder;
using Ordering.Infrastructure.Shared.Dtos.SaleOrder;
using Ordering.Infrastructure.Shared.ValueObjects;

namespace Ordering.Domain.Interfaces.Actors;

public interface IOrderingProcessActor
{
    /// <summary>
    ///     提交订单
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    public Task<Result<OrderRecord>> SubmitAsync(CreateSaleOrderCommand command);
    
    /// <summary>
    /// 获取订单详情
    /// </summary>
    /// <returns></returns>
    public Task<Result<SaleOrderOutputDto>> GetAsync();
}