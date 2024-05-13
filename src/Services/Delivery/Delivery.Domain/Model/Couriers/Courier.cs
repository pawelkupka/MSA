using Common.Domain.Model;

namespace Delivery.Domain.Model.Couriers;

public class Courier(string name, bool available)
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; } = name;
    public bool Available { get; private set; } = available;
    public CourierDeliveryPlan DeliveryPlan { get; } = new CourierDeliveryPlan();

    public static (Courier, List<IDomainEvent>) Create(string name, bool available)
    {
        var courier = new Courier(name, available);
        return (courier, [new CourierCreated(courier.Id, courier.Name, courier.Available, courier.DeliveryPlan)]);
    }

    public IEnumerable<IDomainEvent> MakeAvailable()
    {
        Available = true;
        return [];
    }

    public IEnumerable<IDomainEvent> MakeUnavailable()
    {
        Available = false;
        return [];
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
