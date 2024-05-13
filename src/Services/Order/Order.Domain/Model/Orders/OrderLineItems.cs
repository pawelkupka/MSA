namespace Order.Domain.Model.Orders;

public class OrderLineItems(List<OrderLineItem> lineItems)
{
    public List<OrderLineItem> LineItems { get; } = lineItems;

    public OrderLineItem FindOrderLineItem(int lineItemId)
    {
        return LineItems.Single(x => x.LineItemId == lineItemId);
    }

    public decimal ChangeToOrderTotal(OrderRevision orderRevision)
    {
        return orderRevision.RevisedOrderLineItems.Sum(x => FindOrderLineItem(x.LineItemId).DeltaForChangedQuantity(x.Quantity));
    }

    public void UpdateLineItems(OrderRevision orderRevision)
    {
        orderRevision.RevisedOrderLineItems.ForEach(revised =>
        {
            var lineItemIndex = LineItems.FindIndex(x => x.LineItemId == revised.LineItemId);
            var lineItem = LineItems[lineItemIndex];
            LineItems[lineItemIndex] = new OrderLineItem(lineItem.LineItemId, lineItem.Name, lineItem.Price, revised.Quantity);
        });
    }

    public decimal OrderTotal()
    {
        return LineItems.Sum(x => x.GetTotal());
    }

    public LineItemQuantityChange LineItemQuantityChange(OrderRevision orderRevision)
    {
        var currentOrderTotal = OrderTotal();
        var delta = ChangeToOrderTotal(orderRevision);
        var newOrderTotal = currentOrderTotal + delta;
        return new LineItemQuantityChange(currentOrderTotal, newOrderTotal, delta);
    }
}
