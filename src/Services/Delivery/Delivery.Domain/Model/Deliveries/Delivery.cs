namespace Delivery.Domain.Model.Deliveries;

public class Delivery
{
    public Delivery(
            Guid orderId,
            Guid restaurantId,
            DeliveryPickupAddress pickupAddress,
            DeliveryAddress deliveryAddress)
    {
        Id = Guid.NewGuid();
        OrderId = orderId;
        RestaurantId = restaurantId;
        PickupAddress = pickupAddress;
        DeliveryAddress = deliveryAddress;
        Status = DeliveryStatus.Pending;
        //AddDomainEvent(new DeliveryCreated(DeliveryId, OrderId, RestaurantId, PickupAddress, DeliveryAddress, Status));
    }

    public Guid Id { get; private set; }
    public Guid RestaurantId { get; private set; }
    public Guid OrderId { get; private set; }
    public Guid? CourierId { get; private set; }
    public DateTime? WhenReadyForPickup { get; private set; }
    public DateTime? WhenPickedUp { get; private set; }
    public DateTime? WhenDelivered { get; private set; }
    public DeliveryPickupAddress PickupAddress { get; private set; }
    public DeliveryAddress DeliveryAddress { get; private set; }
    public DeliveryStatus Status { get; private set; }
    public bool HasAssignedCourier => CourierId.HasValue;

    public void Schedule(Guid courierId, DateTime whenReadyForPickup)
    {
        CourierId = courierId;
        WhenReadyForPickup = whenReadyForPickup;
        Status = DeliveryStatus.Scheduled;
        //AddDomainEvent(new DeliveryScheduled(DeliveryId, CourierId, WhenReadyForPickup, Status));
    }

    public void Cancel()
    {
        CourierId = null;
        Status = DeliveryStatus.Cancelled;
        //AddDomainEvent(new DeliveryCancelled(DeliveryId, CourierId, Status));
    }
}
