using Common.Application.Commands;
using Delivery.Domain.Model.Deliveries;
using Delivery.Domain.Model.Restaurants;

namespace Delivery.Application.Commands;

public class CreateDelivery
{
    public record Command(Guid OrderId, Guid RestaurantId, DeliveryAddress DeliveryAddress) : ICommand;

    public class Handler : ICommandHandler<Command>
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly IDeliveryRepository _deliveryRepository;

        public Handler(IRestaurantRepository restaurantRepository, IDeliveryRepository deliveryRepository)
        {
            _restaurantRepository = restaurantRepository;
            _deliveryRepository = deliveryRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository.FindByIdAsync(command.RestaurantId);
            var deliveryPickupAddress = new DeliveryPickupAddress(restaurant.Address.Line1, restaurant.Address.Line2, restaurant.Address.City, restaurant.Address.PostalCode);
            (var delivery, var events) = Domain.Model.Deliveries.Delivery.Create(command.OrderId, command.RestaurantId, deliveryPickupAddress, command.DeliveryAddress);
            await _deliveryRepository.SaveAsync(delivery);
            domainEventPublisher.publish(delivery, events);
        }
    }
}
