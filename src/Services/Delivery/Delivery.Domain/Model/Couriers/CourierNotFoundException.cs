namespace Delivery.Domain.Model.Couriers;

public class CourierNotFoundException(Guid courierId) : Exception($"Courier with id {courierId} not found");