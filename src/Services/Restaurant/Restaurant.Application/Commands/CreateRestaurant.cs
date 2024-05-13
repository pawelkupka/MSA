using Common.Application.Commands;
using Restaurant.Domain.Model.Restaurants;

namespace Restaurant.Application.Commands;

public static class CreateRestaurant
{
    public record Command(string Name, RestaurantMenu Menu) : ICommand;

    public class Handler : ICommandHandler<Command>
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public Handler(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            (var restaurant, var events) = Domain.Model.Restaurants.Restaurant.Create(command.Name, command.Menu);
            await _restaurantRepository.SaveAsync(restaurant);
            domainEventPublisher.publish(restaurant, events);
        }
    }
}
