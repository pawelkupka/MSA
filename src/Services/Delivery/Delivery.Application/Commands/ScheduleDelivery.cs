using Common.Application.Commands;
using Delivery.Domain.Model.Couriers;
using Delivery.Domain.Model.Deliveries;

namespace Delivery.Application.Commands;

public class ScheduleDelivery
{
    public record Command(Guid OrderId, DateTime ReadyBy) : ICommand;

    public class Handler : ICommandHandler<Command>
    {
        private readonly IDeliveryRepository _deliveryRepository;
        private readonly ICourierRepository _courierRepository;

        public Handler(IDeliveryRepository deliveryRepository, ICourierRepository courierRepository)
        {
            _deliveryRepository = deliveryRepository;
            _courierRepository = courierRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var delivery = await _deliveryRepository.FindByOrderIdAsync(command.OrderId);
            var couriers = await _courierRepository.FindAllAvailableAsync();
            var courier = couriers.FirstOrDefault() ?? throw new NoAvailableCourierException();
            courier.AddAction(CourierAction.MakePickup(delivery.Id, delivery.PickupAddress, command.ReadyBy));
            courier.AddAction(CourierAction.MakeDropoff(delivery.Id, delivery.DeliveryAddress, command.ReadyBy.AddMinutes(30)));
            var events = delivery.Schedule(courier.Id, command.ReadyBy);
            domainEventPublisher.publish(delivery, events);
        }
    }
}
