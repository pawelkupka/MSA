namespace Order.Domain.Model.Orders;

public interface IOrderRepository
{
    Task<Order> FindByIdAsync(Guid orderId);
    Task SaveAsync(Order order);
}
