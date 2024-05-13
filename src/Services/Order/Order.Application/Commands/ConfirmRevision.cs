using Common.Application.Commands;
using Order.Domain.Model.Orders;

namespace Order.Application.Commands;

public class ConfirmRevision
{
    public record Command(Guid OrderId, OrderRevision Revision) : ICommand;

    public class Handler : ICommandHandler<Command>
    {
        private readonly IOrderRepository _orderRepository;

        public Handler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.FindByIdAsync(command.OrderId)
                ?? throw new OrderNotFoundException(command.OrderId);
            var events = order.ConfirmRevision(command.Revision);
            await _orderRepository.SaveAsync(order);
            domainEventPublisher.publish(order, events);
        }
    }
}
