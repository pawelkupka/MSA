using Common.Domain.Model;

namespace Delivery.Domain.Model.Deliveries;

public class Delivery(Guid orderId, Guid restaurantId, DeliveryPickupAddress pickupAddress, DeliveryAddress deliveryAddress)
{
    public Guid Id { get; } = Guid.NewGuid();
    public Guid RestaurantId { get; } = restaurantId;
    public Guid OrderId { get; } = orderId;
    public Guid? CourierId { get; private set; }
    public DateTime? WhenReadyForPickup { get; private set; }
    public DateTime? WhenPickedUp { get; private set; }
    public DateTime? WhenDelivered { get; private set; }
    public DeliveryPickupAddress PickupAddress { get; } = pickupAddress;
    public DeliveryAddress DeliveryAddress { get; } = deliveryAddress;
    public DeliveryStatus Status { get; private set; } = DeliveryStatus.Pending;
    public bool HasAssignedCourier => CourierId.HasValue;

    public static (Delivery, List<IDomainEvent>) Create(Guid orderId, Guid restaurantId, DeliveryPickupAddress pickupAddress, DeliveryAddress deliveryAddress)
    {
        var delivery = new Delivery(orderId, restaurantId, pickupAddress, deliveryAddress);
        return (delivery, [new DeliveryCreated(delivery.Id, delivery.OrderId, delivery.RestaurantId, delivery.PickupAddress, delivery.DeliveryAddress, delivery.Status)]);
    }

    public IEnumerable<IDomainEvent> Schedule(Guid courierId, DateTime whenReadyForPickup)
    {
        CourierId = courierId;
        WhenReadyForPickup = whenReadyForPickup;
        Status = DeliveryStatus.Scheduled;
        return [new DeliveryScheduled(Id, CourierId, WhenReadyForPickup, Status)];
    }

    public IEnumerable<IDomainEvent> Cancel()
    {
        CourierId = null;
        Status = DeliveryStatus.Cancelled;
        return [new DeliveryCancelled(Id, CourierId, Status)];
    }
}
