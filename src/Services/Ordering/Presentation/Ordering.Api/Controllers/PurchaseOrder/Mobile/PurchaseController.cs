using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Interfaces;
using Ordering.Domain.Commands.TradeOrder;
using Ordering.Infrastructure.Shared.Dtos.TradeOrder;
using Ordering.Infrastructure.Shared.Records;

namespace Ordering.Api.Controllers.PurchaseOrder.Mobile;

/// <summary>
///     买料订单
/// </summary>
[ApiExplorerSettings(GroupName = AppConstants.MobileGroup)]
[ApiVersion("1.0")]
[AdvertiseApiVersions("1.0")]
public class PurchaseController : MobileApiBaseController
{
    /// <summary>
    ///     获取订单详情
    /// </summary>
    /// <param name="id">订单Id</param>
    /// <param name="apiService"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiVersion("1.0")]
    [ActionName("Get")]
    [ProducesResponseType(typeof(TradeOrderOutputDto), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAsync(string id,
        [FromServices] ITradeOrderService apiService)
    {
        var result = await apiService.GetAsync(id);
        return result.ToOkResult(data => data);
    }

    /// <summary>
    ///     获取订单详情
    /// </summary>
    /// <param name="orderNumber">订单号</param>
    /// <param name="apiService"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiVersion("1.0")]
    [ActionName("Detail")]
    [ProducesResponseType(typeof(TradeOrderOutputDto), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDetailAsync(string orderNumber,
        [FromServices] ITradeOrderService apiService)
    {
        var result = await apiService.GetByOrderNumberAsync(orderNumber);
        return result.ToOkResult(data => data);
    }

    /// <summary>
    ///     提交订单
    /// </summary>
    /// <param name="request"></param>
    /// <param name="apiService"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiVersion("1.0")]
    [ActionName("Submit")]
    [ProducesResponseType(typeof(OrderRecord), 201)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> SubmitAsync(
        [FromBody] CreateTradeOrderCommand request,
        [FromServices] ITradeOrderService apiService)
    {
        var result = await apiService.SubmitAsync(request);

        return result.ToCreatedResult(data => data, "/mobile/tradeOrder/get");
    }
}