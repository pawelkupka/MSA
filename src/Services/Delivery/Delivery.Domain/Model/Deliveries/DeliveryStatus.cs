namespace Delivery.Domain.Model.Deliveries;

public record DeliveryStatus(int Id, string Name)
{
    public static DeliveryStatus Pending = new DeliveryStatus(1, "Pending");
    public static DeliveryStatus Scheduled = new DeliveryStatus(2, "Scheduled");
    public static DeliveryStatus Cancelled = new DeliveryStatus(3, "Cancelled");
}
