using Common.Application.Commands;
using Order.Domain.Model.Restaurants;

namespace Order.Application.Commands;

public class CreateRestaurant
{
    public record Command(Guid RestaurantId, string Name, List<RestaurantMenuItem> MenuItems) : ICommand;

    public class Handler : ICommandHandler<Command>
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public Handler(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var restaurant = new Restaurant(command.RestaurantId, command.Name, command.MenuItems);
            await _restaurantRepository.SaveAsync(restaurant);
        }
    }
}
