namespace Delivery.Domain.Model.Couriers;

public record CourierActionType(int Id, string Name)
{
    public static CourierActionType Pickup = new CourierActionType(1, "Pickup");
    public static CourierActionType Dropoff = new CourierActionType(2, "Dropoff");
}
