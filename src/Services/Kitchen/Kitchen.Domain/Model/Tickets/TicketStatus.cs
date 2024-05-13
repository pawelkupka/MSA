namespace Kitchen.Domain.Model.Tickets;

public enum TicketStatus
{
    CREATE_PENDING, 
    AWAITING_ACCEPTANCE, 
    ACCEPTED, 
    PREPARING, 
    READY_FOR_PICKUP, 
    PICKED_UP, 
    CANCEL_PENDING, 
    CANCELLED, 
    REVISION_PENDING
}
