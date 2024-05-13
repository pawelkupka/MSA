using Common.Domain.Model;

namespace Delivery.Domain.Model.Restaurants;

public class Restaurant(string name, RestaurantAddress address)
{
    public Guid Id { get; } = Guid.NewGuid();
    public string Name { get; } = name;
    public RestaurantAddress Address { get; } = address;

    public static (Restaurant, List<IDomainEvent>) Create(string name, RestaurantAddress address)
    {
        var restaurant = new Restaurant(name, address);
        return (restaurant, [new RestaurantCreated(restaurant.Id, restaurant.Name, restaurant.Address)]);
    }
}
