using Common.Application.Commands;
using Kitchen.Domain.Model.Restaurants;

namespace Kitchen.Application.Commands;

public static class CancelOrder
{
    public record Command(Guid OrderId, bool IsForce) : ICommand;

    public class Handler : ICommandHandler<Command>
    {
        private readonly IRestaurantRepository _restaurantRepository;

        public Handler(IRestaurantRepository restaurantRepository)
        {
            _restaurantRepository = restaurantRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {

        }
    }
}