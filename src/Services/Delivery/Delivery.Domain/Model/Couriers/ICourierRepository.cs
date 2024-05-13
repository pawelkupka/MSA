namespace Delivery.Domain.Model.Couriers;

public interface ICourierRepository
{
    Task<Courier> FindByIdAsync(Guid courierId);
    Task<IEnumerable<Courier>> FindAllAvailableAsync();
    Task SaveAsync(Courier courier);
}
