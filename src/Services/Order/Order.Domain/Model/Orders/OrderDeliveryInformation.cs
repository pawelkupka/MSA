namespace Order.Domain.Model.Orders;

public record OrderDeliveryInformation(DateTime deliveryTime, OrderDeliveryAddress deliveryAddress);
