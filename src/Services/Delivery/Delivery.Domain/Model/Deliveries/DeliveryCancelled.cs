namespace Delivery.Domain.Model.Deliveries;

public record DeliveryCancelled(
    Guid DeliveryId,
    Guid? CourierId,
    DeliveryStatus Status);