namespace Order.Domain.Model.Restaurants;

public class RestaurantNotFoundException : Exception
{
    public RestaurantNotFoundException(Guid restaurantId) : base($"Restaurant not found with id " + restaurantId) { }
}
