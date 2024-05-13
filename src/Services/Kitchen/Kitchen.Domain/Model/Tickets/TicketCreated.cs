using Common.Domain.Model;

namespace Kitchen.Domain.Model.Tickets;

public record TicketCreated(Guid Td, TicketDetails Details) : IDomainEvent;