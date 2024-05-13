namespace Order.Domain.Model.Orders;

public class OrderNotFoundException(Guid ticketId) : Exception($"Order with id {ticketId} not found");
