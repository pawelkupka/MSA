namespace Order.Domain.Model.Orders;

public record OrderLineItem(int LineItemId, string Name, decimal Price, int Quantity)
{
    public decimal DeltaForChangedQuantity(int newQuantity)
    {
        return Price * (newQuantity - Quantity);
    }

    public decimal GetTotal()
    {
        return Price * Quantity;
    }
}