namespace Kitchen.Domain.Model.Restaurants;

public class TicketNotFoundException(Guid ticketId) : Exception($"Ticket with id {ticketId} not found");