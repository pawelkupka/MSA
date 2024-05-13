using Common.Application.Commands;
using Order.Domain.Model.Restaurants;

namespace Order.Application.Commands;

public class ReviseMenu
{
    public record Command(Guid RestaurantId, List<RestaurantMenuItem> MenuItems) : ICommand;

    public class Handler : ICommandHandler<Command>
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public Handler(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository.FindByIdAsync(command.RestaurantId);
            var events = restaurant.ReviseMenu(command.MenuItems);
            await _restaurantRepository.SaveAsync(restaurant);
            domainEventPublisher.publish(restaurant, events);
        }
    }
}
