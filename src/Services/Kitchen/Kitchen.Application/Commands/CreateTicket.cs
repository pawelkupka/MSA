using Common.Application.Commands;
using Kitchen.Domain.Model.Tickets;

namespace Kitchen.Application.Commands;

public static class CreateTicket
{
    public record Command(Guid RestaurantId, Guid TicketId, TicketDetails TicketDetails) : ICommand;

    public class Handler : ICommandHandler<Command>
    {
        private readonly ITicketRepository _ticketRepository;

        public Handler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            (var ticket, var events) = Ticket.Create(command.RestaurantId, command.TicketId, command.TicketDetails);
            await _ticketRepository.SaveAsync(ticket);
            domainEventPublisher.publish(ticket, events);
        }
    }
}
