using LanguageExt.Common;
using Ordering.Domain.Commands.TradeOrder;
using Ordering.Infrastructure.Shared.Dtos.TradeOrder;
using Ordering.Infrastructure.Shared.Records;

namespace Ordering.Application.Interfaces;

public interface ITradeOrderService
{
    public Task<Result<OrderRecord>> SubmitAsync(CreateTradeOrderCommand orderCommand);
    public Task<Result<TradeOrderOutputDto>> GetAsync(string id);
    public Task<Result<TradeOrderOutputDto>> GetByOrderNumberAsync(string orderNumber);
}