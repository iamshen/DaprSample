using LanguageExt.Common;
using Ordering.Domain.Interfaces.Commands.PurchaseOrder;
using Ordering.Infrastructure.Shared.Dtos.PurchaseOrder;
using Ordering.Infrastructure.Shared.Records;

namespace Ordering.Application.Interfaces;

public interface IPurchaseOrderApiService
{
    public Task<Result<OrderRecord>> SubmitAsync(CreateOrderCommand orderCommand);
    public Task<Result<PurchaseOrderOutputDto>> GetAsync(string id);
    public Task<Result<PurchaseOrderOutputDto>> GetByOrderNumberAsync(string orderNumber);
}