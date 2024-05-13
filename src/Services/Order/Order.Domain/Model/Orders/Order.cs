using Common.Domain.Exceptions;
using Common.Domain.Model;

namespace Order.Domain.Model.Orders;

public class Order(Guid consumerId, Guid restaurantId, OrderDeliveryInformation deliveryInformation, OrderLineItems orderLineItems)
{
    public Guid ConsumerId { get; } = consumerId;
    public Guid RestaurantId { get; } = restaurantId;
    public OrderDeliveryInformation DeliveryInformation { get; private set; } = deliveryInformation;
    public OrderLineItems OrderLineItems { get; } = orderLineItems;
    public OrderStatus Status { get; private set; } = OrderStatus.APPROVAL_PENDING;
    public decimal OrderMaximum { get; private set; } = decimal.MaxValue;

    public static (Order, List<IDomainEvent>) Create(Guid consumerId, Guid restaurantId, OrderDeliveryInformation deliveryInformation, OrderLineItems orderLineItems)
    {
        var order = new Order(consumerId, restaurantId, deliveryInformation, orderLineItems);
        return (order, [new OrderCreated(consumerId, restaurantId, deliveryInformation, orderLineItems)]);
    }

    public decimal GetOrderTotal()
    {
        return OrderLineItems.OrderTotal();
    }

    public IEnumerable<IDomainEvent> Cancel()
    {
        switch (Status)
        {
            case OrderStatus.APPROVED:
                Status = OrderStatus.CANCEL_PENDING;
                return [];
            default:
                throw new UnsupportedStateTransitionException(Status);
        }
    }

    public IEnumerable<IDomainEvent> UndoPendingCancel()
    {
        switch (Status)
        {
            case OrderStatus.CANCEL_PENDING:
                Status = OrderStatus.APPROVED;
                return [];
            default:
                throw new UnsupportedStateTransitionException(Status);
        }
    }

    public IEnumerable<IDomainEvent> NoteCancelled()
    {
        switch (Status)
        {
            case OrderStatus.CANCEL_PENDING:
                Status = OrderStatus.CANCELLED;
                return [new OrderCancelled()];
            default:
                throw new UnsupportedStateTransitionException(Status);
        }
    }

    public IEnumerable<IDomainEvent> NoteApproved()
    {
        switch (Status)
        {
            case OrderStatus.APPROVAL_PENDING:
                Status = OrderStatus.APPROVED;
                return [new OrderAuthorized()];
            default:
                throw new UnsupportedStateTransitionException(Status);
        }
    }

    public IEnumerable<IDomainEvent> NoteRejected()
    {
        switch (Status)
        {
            case OrderStatus.APPROVAL_PENDING:
                Status = OrderStatus.REJECTED;
                return [new OrderRejected()];
            default:
                throw new UnsupportedStateTransitionException(Status);
        }
    }

    public IEnumerable<IDomainEvent> NoteReversingAuthorization()
    {
        throw new NotImplementedException();
    }

    public (LineItemQuantityChange, IEnumerable<IDomainEvent>) Revise(OrderRevision orderRevision)
    {
        switch (Status)
        {
            case OrderStatus.APPROVED:
                var change = OrderLineItems.LineItemQuantityChange(orderRevision);
                if (change.NewOrderTotal >= OrderMaximum)
                {
                    throw new OrderMaximumNotMetException();
                }
                Status = OrderStatus.REVISION_PENDING;
                return new (change, [new OrderRevisionProposed(orderRevision, change.CurrentOrderTotal, change.NewOrderTotal)]);
            default:
                throw new UnsupportedStateTransitionException(Status);
        }
    }

    public IEnumerable<IDomainEvent> RejectRevision()
    {
        switch (Status)
        {
            case OrderStatus.REVISION_PENDING:
                Status = OrderStatus.APPROVED;
                return [];
            default:
                throw new UnsupportedStateTransitionException(Status);
        }
    }

    public IEnumerable<IDomainEvent> ConfirmRevision(OrderRevision orderRevision)
    {
        switch (Status)
        {
            case OrderStatus.REVISION_PENDING:
                DeliveryInformation = orderRevision.DeliveryInformation;
                if (orderRevision.RevisedOrderLineItems.Any())
                {
                    OrderLineItems.UpdateLineItems(orderRevision);
                }
                Status = OrderStatus.APPROVED;
                var change = OrderLineItems.LineItemQuantityChange(orderRevision);
                return [new OrderRevised(orderRevision, change.CurrentOrderTotal, change.NewOrderTotal)];
            default:
                throw new UnsupportedStateTransitionException(Status);
        }
    }
}
