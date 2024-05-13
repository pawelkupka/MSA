using Common.Application.Commands;
using Kitchen.Domain.Model.Restaurants;
using Kitchen.Domain.Model.Tickets;

namespace Kitchen.Application.Commands;

public static class AcceptTicket
{
    public record Command(Guid TicketId, DateTime ReadyBy) : ICommand;

    public class Handler : ICommandHandler<Command>
    {
        private readonly ITicketRepository _ticketRepository;

        public Handler(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        public async Task Handle(Command command, CancellationToken cancellationToken)
        {
            var ticket = await _ticketRepository.FindByIdAsync(command.TicketId) 
                ?? throw new TicketNotFoundException(command.TicketId);
            var events = ticket.Accept(command.ReadyBy);
            await _ticketRepository.SaveAsync(ticket);
            domainEventPublisher.publish(ticket, events);
        }
    }
}
