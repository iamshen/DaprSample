using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Interfaces;
using Ordering.Domain.Interfaces.Commands.TradeOrder;

namespace Ordering.Api.Controllers.TradeOrder.Mobile;

/// <summary>
///  买卖料订单
/// </summary>
[ControllerName("TradeOrder")]
[ApiExplorerSettings(GroupName = AppConstants.MobileGroup)]
public class TradeOrderController : MobileApiBaseController
{
    /// <summary>
    ///     创建交易单
    /// </summary>
    /// <param name="request"></param>
    /// <param name="apiService"></param>
    /// <returns></returns>
    [HttpPost]
    [ApiVersion("1.0")]
    [ActionName("submit")]
    [ProducesResponseType(typeof(string), 200)]
    public async Task<IActionResult> SubmitAsync(
        [FromBody] CreateTradeOrderCommand request, 
        [FromServices] ITradeOrderService apiService)
    {
        var response = await apiService.SubmitAsync(request);

        return new OkObjectResult(response);
    }
}