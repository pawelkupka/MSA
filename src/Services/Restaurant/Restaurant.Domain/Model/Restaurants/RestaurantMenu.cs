namespace Restaurant.Domain.Model.Restaurants;

public record RestaurantMenu(IEnumerable<RestaurantMenuItem> MenuItems);