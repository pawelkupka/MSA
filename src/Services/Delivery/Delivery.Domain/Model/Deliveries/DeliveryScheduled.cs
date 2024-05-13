using Common.Domain.Model;

namespace Delivery.Domain.Model.Deliveries;

public record DeliveryScheduled(
    Guid DeliveryId, 
    Guid? CourierId, 
    DateTime? WhenReadyForPickup, 
    DeliveryStatus Status) : IDomainEvent;