using Common.Domain.Model;

namespace Kitchen.Domain.Model.Restaurants;

public class Restaurant(Guid Id, List<RestaurantMenuItem> MenuItems)
{
    public List<IDomainEvent> ReviseMenu(RestaurantMenu revisedMenu)
    {
        throw new NotImplementedException();
    }

    public void VerifyRestaurantDetails(TicketDetails ticketDetails)
    {
        throw new NotImplementedException();
    }
}
