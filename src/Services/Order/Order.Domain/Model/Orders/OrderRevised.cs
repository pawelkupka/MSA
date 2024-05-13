using Common.Domain.Model;

namespace Order.Domain.Model.Orders;

public record OrderRevised(OrderRevision OrderRevision, decimal CurrentOrderTotal, decimal NewOrderTotal) : IDomainEvent;