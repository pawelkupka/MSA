using Common.Domain.Model;

namespace Kitchen.Domain.Model.Tickets;

public record TicketAccepted(DateTime ReadyBy) : IDomainEvent;
