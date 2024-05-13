using Common.Domain.Model;

namespace Delivery.Domain.Model.Deliveries;

public record DeliveryCreated(
    Guid DeliveryId, 
    Guid OrderId, 
    Guid RestaurantId, 
    DeliveryPickupAddress PickupAddress, 
    DeliveryAddress DeliveryAddress, 
    DeliveryStatus Status) : IDomainEvent;