namespace Order.Domain.Model.Orders;

public enum OrderStatus
{
    APPROVAL_PENDING,
    APPROVED,
    REJECTED,
    CANCEL_PENDING,
    CANCELLED,
    REVISION_PENDING
}
