using Common.Domain.Model;

namespace Order.Domain.Model.Orders;

public record OrderCreated(Guid ConsumerId, Guid RestaurantId, OrderDeliveryInformation DeliveryInformation, OrderLineItems OrderLineItems) : IDomainEvent;
