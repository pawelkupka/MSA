namespace Delivery.Domain.Model.Restaurants;

public interface IRestaurantRepository
{
    Task<Restaurant> FindByIdAsync(Guid restaurantId);
    Task AddAsync(Restaurant restaurant);
    Task UpdateAsync(Restaurant restaurant);
}
