namespace Order.Domain.Model.Orders;

public record OrderRevision(OrderDeliveryInformation DeliveryInformation, List<RevisedOrderLineItem> RevisedOrderLineItems);