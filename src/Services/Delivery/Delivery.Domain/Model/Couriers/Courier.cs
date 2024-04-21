namespace Delivery.Domain.Model.Couriers;

public class Courier
{
    public Courier(string name, bool available)
    {
        Id = Guid.NewGuid();
        Name = name;
        Available = available;
        DeliveryPlan = new CourierDeliveryPlan();
    }

    public Guid Id { get; }
    public string Name { get; }
    public bool Available { get; private set; }
    public CourierDeliveryPlan DeliveryPlan { get; }

    public void MakeAvailable()
    {
        Available = true;
    }

    public void MakeUnavailable()
    {
        Available = false;
    }

    public void AddAction(CourierAction action)
    {
        DeliveryPlan.AddAction(action);
    }

    public void RemoveDelivery(Guid deliveryId)
    {
        DeliveryPlan.RemoveDelivery(deliveryId);
    }

    public IEnumerable<CourierAction> ActionsForDelivery(Guid deliveryId)
    {
        return DeliveryPlan.ActionsForDelivery(deliveryId);
    }
}
