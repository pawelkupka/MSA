namespace Delivery.Domain.Model.Deliveries;

public interface IDeliveryRepository
{
    Task<Delivery> FindByIdAsync(Guid deliveryId);
    Task<Delivery> FindByOrderIdAsync(Guid orderId);
    Task AddAsync(Delivery delivery);
    Task UpdateAsync(Delivery delivery);
}
