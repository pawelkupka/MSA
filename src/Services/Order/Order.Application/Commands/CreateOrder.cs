using Common.Application.Commands;
using Order.Domain.Model.Orders;
using Order.Domain.Model.Restaurants;

namespace Order.Application.Commands;

public class CreateOrder
{
    public record Command(
        Guid ConsumerId, 
        Guid RestaurantId, 
        OrderDeliveryInformation DeliveryInformation, 
        List<MenuItemIdAndQuantity> lineItems) : ICommand;

    public class Handler : ICommandHandler<Command>
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IRestaurantRepository _restaurantRepository;

        public Handler(IOrderRepository orderRepository, IRestaurantRepository restaurantRepository)
        {
            _orderRepository = orderRepository;
            _restaurantRepository = restaurantRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var restaurant = await _restaurantRepository.FindByIdAsync(command.RestaurantId) 
                ?? throw new RestaurantNotFoundException(command.RestaurantId);
            var orderLineItems = makeOrderLineItems(lineItems, restaurant);
            (var order, var events) = Domain.Model.Orders.Order.Create(command.ConsumerId, command.RestaurantId, command.DeliveryInformation, orderLineItems);
            await _orderRepository.SaveAsync(order);
            domainEventPublisher.publish(order, events);
        }

        private OrderLineItems makeOrderLineItems(List<MenuItemIdAndQuantity> lineItems, Restaurant restaurant)
        {
            return lineItems.stream().map(li-> {
                MenuItem om = restaurant.findMenuItem(li.getMenuItemId()).orElseThrow(()-> new InvalidMenuItemIdException(li.getMenuItemId()));
                return new OrderLineItem(li.getMenuItemId(), om.getName(), om.getPrice(), li.getQuantity());
            }).collect(toList());
        }
    }
}
