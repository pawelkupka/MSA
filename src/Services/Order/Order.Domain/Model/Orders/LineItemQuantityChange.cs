namespace Order.Domain.Model.Orders;

public record LineItemQuantityChange(decimal CurrentOrderTotal, decimal NewOrderTotal, decimal Delta);
