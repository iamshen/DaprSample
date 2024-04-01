using LanguageExt.Common;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Interfaces;
using Ordering.Domain.Core.Commands.TradeOrder;
using Ordering.Infrastructure.Shared.Dtos.TradeOrder;
using Ordering.Infrastructure.Shared.ValueObjects;

namespace Ordering.Api.Controllers.TradeOrder.Mobile;

/// <summary>
///     买卖料订单
/// </summary>
[ControllerName("tradeOrder")]
[ApiExplorerSettings(GroupName = AppConstants.MobileGroup)]
public class TradeOrderController : MobileApiBaseController
{
    /// <summary>
    ///     获取订单详情
    /// </summary>
    /// <param name="id">订单Id</param>
    /// <param name="apiService"></param>
    /// <returns></returns>
    [HttpGet]
    [ApiVersion("1.0")]
    [ActionName("get")]
    [ProducesResponseType(typeof(Result<TradeOrderOutputDto>), 200)]
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
    [ActionName("detail")]
    [ProducesResponseType(typeof(Result<TradeOrderOutputDto>), 200)]
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
    [ActionName("submit")]
    [ProducesResponseType(typeof(Result<OrderRecord>), 200)]
    public async Task<IActionResult> SubmitAsync(
        [FromBody] CreateTradeOrderCommand request,
        [FromServices] ITradeOrderService apiService)
    {
        var result = await apiService.SubmitAsync(request);

        return result.ToCreatedResult(data => data, "/mobile/tradeOrder/get");
    }
}