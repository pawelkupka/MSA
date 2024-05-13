using Common.Domain.Model;

namespace Order.Domain.Model.Orders;

public record OrderRevisionProposed(OrderRevision OrderRevision, decimal CurrentOrderTotal, decimal NewOrderTotal) : IDomainEvent;
