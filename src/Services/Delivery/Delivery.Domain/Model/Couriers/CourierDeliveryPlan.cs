namespace Delivery.Domain.Model.Couriers;

public class CourierDeliveryPlan
{
    public CourierDeliveryPlan()
    {
        Actions = new List<CourierAction>();
    }

    public List<CourierAction> Actions { get; }

    public void AddAction(CourierAction action)
    {
        Actions.Append(action);
    }

    public void RemoveDelivery(Guid deliveryId)
    {
        Actions.RemoveAll(action => action.IsForDelivery(deliveryId));
    }

    public IEnumerable<CourierAction> ActionsForDelivery(Guid deliveryId)
    {
        return Actions.Where(action => action.IsForDelivery(deliveryId));
    }
}
