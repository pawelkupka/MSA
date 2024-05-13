using Common.Application.Commands;
using Delivery.Domain.Model.Restaurants;

namespace Delivery.Application.Commands;

public class CreateRestaurant
{
    public record Command(string Name, RestaurantAddress Address) : ICommand;

    public class Handler : ICommandHandler<Command>
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public Handler(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            (var restaurant, var events) = Restaurant.Create(command.Name, command.Address);
            await _restaurantRepository.SaveAsync(restaurant);
            domainEventPublisher.publish(restaurant, events);
        }
    }
}
