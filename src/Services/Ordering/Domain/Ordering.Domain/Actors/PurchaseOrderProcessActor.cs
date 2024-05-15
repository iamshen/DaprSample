using Dapr.Actors.Runtime;
using DaprTool.BuildingBlocks.Domain.Actors;
using DaprTool.BuildingBlocks.Domain.Events;
using DaprTool.BuildingBlocks.Utils;
using DaprTool.BuildingBlocks.Utils.Enumerations;
using DaprTool.BuildingBlocks.Utils.Exceptions;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Ordering.Domain.Interfaces.Actors;
using Ordering.Domain.Interfaces.Commands.PurchaseOrder;
using Ordering.Domain.Interfaces.Events.PurchaseOrder;
using Ordering.Domain.State;
using Ordering.Infrastructure.Shared.Dtos.PurchaseOrder;
using Ordering.Infrastructure.Shared.Enumerations.PurchaseOrder;
using Ordering.Infrastructure.Shared.Options;
using Ordering.Infrastructure.Shared.Records;

namespace Ordering.Domain.Actors;

/// <summary>
///     买料订单
/// </summary>
public class PurchaseOrderProcessActor : DomainActor<PurchaseOrderState>, IPurchaseOrderProcessActor, IRemindable
{
    #region 初始化

    /// <summary>
    ///     ctor
    /// </summary>
    /// <param name="host"></param>
    /// <param name="serviceProvider"></param>
    public PurchaseOrderProcessActor(ActorHost host, IServiceProvider serviceProvider) : base(host, serviceProvider)
    {
    }

    #endregion

    #region 常量

    /// <summary>
    ///     异常错误
    /// </summary>
    internal static class Errors
    {
        /// <summary> 订单已创建，请勿重复操作 </summary>
        public const string RepeatCreated = "订单已创建，请勿重复操作";

        /// <summary> 订单已超时，系统自动取消订单 </summary>
        public const string GracePeriodElapsed = "订单已超时，系统自动取消订单";
    }

    internal const string GracePeriodElapsedReminder = "GracePeriodElapsed"; // 订单超时提醒器
    internal const int PurchaseOrderBizType = 10; // current biz is 10

    #endregion

    #region 属性

    private IOptions<OrderingSettings> Settings => ServiceProvider.GetRequiredService<IOptions<OrderingSettings>>();

    #endregion

    #region 提交订单

    /// <summary>
    ///     提交订单
    /// </summary>
    /// <param name="command"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<OrderRecord> SubmitAsync(CreateOrderCommand command)
    {
        // 验证入参
        var validator = ServiceProvider.GetRequiredService<IValidator<CreateOrderCommand>>();
        await validator.ValidateAndThrowGlobalAsync(command);

        // 获取状态
        var stateObj = await StateManager.TryGetStateAsync<PurchaseOrderState>(StateDataKey);
        if (stateObj.HasValue) throw new Exception(Errors.RepeatCreated);

        // 生成单号
        var orderNumberProxy = GetSerialNumberService();
        var orderNo = await orderNumberProxy.GenerateAsync(OrderType.PurchaseOrder, PurchaseOrderBizType);

        // 更新状态
        var state = new PurchaseOrderState
        {
            OrderNo = orderNo,
            OrderStatus = OrderStatus.Submitted,
            PayStatus = PayStatus.Created,
            TradeStatus = TradeStatus.Created,
            Buyer = command.Buyer,
            Seller = command.Seller,
            SaleStore = command.SaleStore,
            Remark = command.Remark ?? OrderStatus.Created.GetDisplayName(),
            CreatedTime = DateTimeOffset.Now,
            UpdatedTime = DateTimeOffset.Now,
            OrderItems = command.OrderItems
        };
        await StateManager.SetStateAsync(StateDataKey, state);

        // 注册提醒器
        await RegisterReminderAsync(
            GracePeriodElapsedReminder,
            null,
            // dueTime - 首次调用提醒前的延迟时间。指定负 1 (-1) 毫秒可禁用调用。 指定零 (0) 表示在注册后立即调用提醒。
            // Actor 激活后， Settings.Value.GracePeriodTime 分钟内将会第一次触发提醒器
            TimeSpan.FromMinutes(Settings.Value.GracePeriodTime),
            // period - 首次调用后调用提醒的时间间隔。指定负一 (-1) 毫秒可禁用定期调用。
            // 每15分钟执行一次。
            TimeSpan.FromMinutes(Settings.Value.GracePeriodTime));

        // 发布事件（便于持久化到业务库）
        var events = new List<IntegrationEvent>
        {
            new OrderSubmittedEvent
            {
                ActorId = ActorId,
                OrderNo = state.OrderNo,
                OrderStatus = state.OrderStatus,
                PayStatus = state.PayStatus,
                TradeStatus = state.TradeStatus,
                Buyer = state.Buyer,
                Seller = state.Seller,
                SaleStore = state.SaleStore,
                Remark = state.Remark,
                CreatedTime = state.CreatedTime,
                UpdatedTime = state.UpdatedTime,
                OrderItems = state.OrderItems
            },

        };
        await RaiseEvents(events.ToArray());

        return new OrderRecord(ActorId, orderNo);
    }

    /// <summary>
    ///     获取订单明细
    /// </summary>
    /// <returns></returns>
    public async Task<PurchaseOrderOutputDto> GetAsync()
    {
        var order = await StateManager.TryGetStateAsync<PurchaseOrderState>(StateDataKey);
        if (!order.HasValue) throw new GlobalException(ErrorCode.OrderNotExists, ActorId);
        return new PurchaseOrderOutputDto
        {
            OrderId = ActorId,
            OrderNo = order.Value.OrderNo,
            OrderStatus = order.Value.OrderStatus,
            PayStatus = order.Value.PayStatus,
            TradeStatus = order.Value.TradeStatus,
            Buyer = order.Value.Buyer,
            Seller = order.Value.Seller,
            SaleStore = order.Value.SaleStore,
            Remark = order.Value.Remark,
            CreatedTime = order.Value.CreatedTime,
            UpdatedTime = order.Value.UpdatedTime,
            OrderItems = order.Value.OrderItems
        };
    }

    #endregion

    #region 私有方法

    /// <summary>
    ///     获取取消订单的事件
    /// </summary>
    /// <param name="reason"></param>
    /// <returns></returns>
    private async Task<IList<IntegrationEvent>> GetCancelEvents(string reason)
    {
        Console.WriteLine("11111111111");
        var order = await StateManager.GetStateAsync<PurchaseOrderState>(StateDataKey);

        var events = new List<IntegrationEvent>
        {
            new OrderStatusChangeToCancelEvent { ActorId = ActorId, OrderNo = order.OrderNo, Remark = reason }
        };
        Console.WriteLine("22222222222222222");

        return events;
    }

    /// <summary>
    ///     尝试更新订单状态，如果预期订单状态与 Actor 中 的 State 不符，那么返回 false
    /// </summary>
    /// <param name="expectedOrderStatus">预期订单状态</param>
    /// <param name="newOrderStatus">新订单状态</param>
    /// <returns></returns>
    private async Task<bool> TryUpdateOrderStatusAsync(OrderStatus expectedOrderStatus, OrderStatus newOrderStatus)
    {
        if (newOrderStatus == expectedOrderStatus)
        {
            Logger.LogWarning("操作没有引发变化");
            return false;
        }

        var order = await StateManager.TryGetStateAsync<PurchaseOrderState>(StateDataKey);
        if (!order.HasValue)
        {
            Logger.LogWarning("订单: {OrderId} 不存在，无法更新", ActorId);
            return false;
        }


        if (order.Value.OrderStatus != expectedOrderStatus)
        {
            Logger.LogWarning("订单: {OrderId} 的订单状态处于 {Status}，而非预期状态 {ExpectedStatus}",
                ActorId, order.Value.OrderStatus.GetDisplayName(), expectedOrderStatus.GetDisplayName());

            return false;
        }


        order.Value.OrderStatus = newOrderStatus;

        await StateManager.SetStateAsync(StateDataKey, order);

        return true;
    }

    #endregion

    #region 提醒器

    /// <summary>
    ///     提醒器
    /// </summary>
    /// <param name="reminderName"></param>
    /// <param name="state"></param>
    /// <param name="dueTime"></param>
    /// <param name="period"></param>
    /// <returns></returns>
    public Task ReceiveReminderAsync(string reminderName, byte[] state, TimeSpan dueTime, TimeSpan period)
    {
        Logger.LogInformation(
            "收到 {Actor}[{ActorId}] 提醒器: {Reminder}",
            nameof(PurchaseOrderProcessActor), ActorId, reminderName);

        // 根据不同的 提醒器名称 处理不同的业务
        return reminderName switch
        {
            GracePeriodElapsedReminder => OnGracePeriodElapsedAsync(),
            _ => Task.CompletedTask
        };
    }

    /// <summary>
    ///     订单超时处理
    /// </summary>
    private async Task OnGracePeriodElapsedAsync()
    {
        // 订单创建 15 分钟后，如果：
        // 订单状态还是 Submitted, 支付状态是 Created, 交易状态是 Created
        // 那么意味着订单已超时，可以取消订单。
        var order = await StateManager.TryGetStateAsync<PurchaseOrderState>(StateDataKey);
        if (!order.HasValue)
        {
            Logger.LogWarning("订单: {OrderId} 不存在，取消超时处理", ActorId);
            return;
        }

        if (order.Value.OrderStatus is OrderStatus.Submitted &&
            order.Value.PayStatus is PayStatus.Created &&
            order.Value.TradeStatus is TradeStatus.Created)
        {
            var statusChanged = await TryUpdateOrderStatusAsync(OrderStatus.Submitted, OrderStatus.Cancelled);
            if (statusChanged)
                await RaiseEvents(new List<IntegrationEvent>
                {
                    new OrderStatusChangeToCancelEvent
                    {
                        ActorId = ActorId,
                        OrderNo = order.Value.OrderNo,
                        Remark = Errors.GracePeriodElapsed
                    }
                }.ToArray());
        }

        // 卸载提醒器
        await UnregisterReminderAsync(GracePeriodElapsedReminder);
    }

    #endregion
}