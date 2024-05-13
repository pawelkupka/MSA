namespace Kitchen.Domain.Model.Tickets;

public interface ITicketRepository
{
    Task<Ticket> FindByIdAsync(Guid ticketId);
    Task SaveAsync(Ticket ticket);
}
