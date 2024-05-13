using Common.Domain.Model;

namespace Restaurant.Domain.Model.Restaurants;

public class Restaurant(string name, RestaurantMenu menu)
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; } = name ?? throw new ArgumentNullException("name");
    public RestaurantMenu Menu { get; } = menu ?? throw new ArgumentNullException("menu");

    public static (Restaurant, List<IDomainEvent>) Create(string name, RestaurantMenu menu)
    {
        var restaurant = new Restaurant(name, menu);
        return (restaurant, [new RestaurantCreated(restaurant.Id, restaurant.Name, restaurant.Menu)]);
    }
}
