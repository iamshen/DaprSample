using MediatR;
using Ordering.Domain.Interfaces.Events.PurchaseOrder;

namespace Ordering.Domain.Interfaces.Observer.PurchaseOrder;

public interface IPurchaseOrderDbEventHandler :
    INotificationHandler<OrderSubmittedEvent>,
    INotificationHandler<OrderSubmittedToPayEvent>,
    INotificationHandler<OrderStatusChangeToCancelEvent>;