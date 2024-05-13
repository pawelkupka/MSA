namespace Kitchen.Domain.Model.Restaurants;

public interface IRestaurantRepository
{
    Task<Restaurant> FindByIdAsync(Guid restaurantId);
    Task SaveAsync(Restaurant restaurant);
}
