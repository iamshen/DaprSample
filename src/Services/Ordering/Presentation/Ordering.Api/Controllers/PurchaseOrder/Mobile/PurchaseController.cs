﻿using Asp.Versioning;
using DaprTool.BuildingBlocks.Utils.ValueObjects;
using Microsoft.AspNetCore.Mvc;
using Ordering.Application.Interfaces;
using Ordering.Domain.Interfaces.Commands.PurchaseOrder;
using Ordering.Infrastructure.Shared.Dtos.PurchaseOrder;
using Ordering.Infrastructure.Shared.Records;

namespace Ordering.Api.Controllers.PurchaseOrder.Mobile;

/// <summary>
///     买料订单
/// </summary>
[ApiExplorerSettings(GroupName = AppConstants.MobileGroup)]
[ApiVersion("1.0")]
[AdvertiseApiVersions("1.0")]
public class PurchaseController(IPurchaseOrderApiService apiApiService) : MobileApiBaseController
{
    /// <summary>
    ///     获取订单详情
    /// </summary>
    /// <param name="id">订单Id</param>
    /// <returns></returns>
    [HttpGet("Get")]
    [ProducesResponseType(typeof(ResponseResult<PurchaseOrderOutputDto>), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetAsync([FromQuery]string id)
    {
        var result = await apiApiService.GetAsync(id);
        return result.ToOkResult(data => data);
    }

    /// <summary>
    ///     获取订单详情
    /// </summary>
    /// <param name="orderNumber">订单号</param>
    /// <returns></returns>
    [HttpGet("Detail")]
    [ProducesResponseType(typeof(ResponseResult<PurchaseOrderOutputDto>), 200)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetDetailAsync(string orderNumber)
    {
        var result = await apiApiService.GetByOrderNumberAsync(orderNumber);
        return result.ToOkResult(data => data);
    }

    /// <summary>
    ///     提交订单
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost("Submit")]
    [ProducesResponseType(typeof(ResponseResult<OrderRecord>), 200)]
    public async Task<IActionResult> SubmitAsync([FromBody] CreateOrderCommand request)
    {
        var result = await apiApiService.SubmitAsync(request);

        return result.ToOkResult(data => data);
    }
}