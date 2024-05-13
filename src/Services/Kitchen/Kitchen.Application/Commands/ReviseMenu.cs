using Common.Application.Commands;
using Kitchen.Domain.Model;
using Kitchen.Domain.Model.Restaurants;

namespace Kitchen.Application.Commands;

public static class ReviseMenu
{
    public record Command(Guid RestaurantId, RestaurantMenu RevisedMenu) : ICommand;

    public class Handler : ICommandHandler<Command>
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public Handler(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository.FindByIdAsync(command.RestaurantId) 
                ?? throw new RestaurantIdNotFoundException(command.RestaurantId);
            restaurant.ReviseMenu(command.RevisedMenu);
            await _restaurantRepository.SaveAsync(restaurant);
        }
    }
}
