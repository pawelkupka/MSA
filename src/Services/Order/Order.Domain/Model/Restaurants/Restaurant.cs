using Common.Domain.Model;

namespace Order.Domain.Model.Restaurants;

public class Restaurant(Guid id, string name, List<RestaurantMenuItem> menuItems)
{
    public Guid Id { get; } = id;
    public string Name { get; } = name;
    public List<RestaurantMenuItem> MenuItems { get; } = menuItems;

    public List<IDomainEvent> ReviseMenu(List<RestaurantMenuItem> revisedMenu)
    {
        throw new NotImplementedException();
    }

    public void VerifyRestaurantDetails(RestaurantDetails restaurantDetails)
    {
        throw new NotImplementedException();
    }

    public RestaurantMenuItem? FindMenuItem(int menuItemId)
    {
        return MenuItems.FirstOrDefault(x => x.Id == menuItemId);
    }
}
