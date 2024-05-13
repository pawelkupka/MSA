using Common.Application.Commands;
using Kitchen.Domain.Model.Restaurants;

namespace Kitchen.Application.Commands;

public static class CreateMenu
{
    public record Command(Guid Id, RestaurantMenu Menu) : ICommand;

    public class Handler : ICommandHandler<Command>
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public Handler(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var restaurant = new Restaurant(command.Id, command.Menu.MenuItems);
            await _restaurantRepository.SaveAsync(restaurant);
        }
    }
}
