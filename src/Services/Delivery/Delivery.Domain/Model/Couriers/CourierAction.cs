namespace Delivery.Domain.Model.Couriers;

public record CourierAction(CourierActionType Type, Guid DeliveryId, CourierActionAddress Address, DateTime Time)
{
    public bool IsForDelivery(Guid deliveryId)
    {
        return DeliveryId.Equals(deliveryId);
    }

    public static CourierAction MakePickup(Guid deliveryId, CourierActionAddress address, DateTime time)
    {
        return new CourierAction(CourierActionType.Pickup, deliveryId, address, time);
    }

    public static CourierAction MakeDropoff(Guid deliveryId, CourierActionAddress address, DateTime time)
    {
        return new CourierAction(CourierActionType.Dropoff, deliveryId, address, time);
    }
}
