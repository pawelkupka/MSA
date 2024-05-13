using Common.Application.Commands;
using Order.Domain.Model.Orders;

namespace Order.Application.Commands;

public class ConfirmChangeLineItemQuantity
{
    public record Command(Guid TicketId, DateTime ReadyBy) : ICommand;

    public class Handler : ICommandHandler<Command>
    {
        private readonly IOrderRepository _orderRepository;

        public Handler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var order = orderRepository.findById(orderId).map(order -> {
                List<OrderDomainEvent> events = order.confirmRevision(orderRevision);
                orderAggregateEventPublisher.publish(order, events);
            });
        }
    }
}
