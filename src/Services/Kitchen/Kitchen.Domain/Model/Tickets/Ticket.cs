using Common.Domain.Exceptions;
using Common.Domain.Model;

namespace Kitchen.Domain.Model.Tickets;

public class Ticket
{
    public Guid Id { get; private set; }
    public TicketStatus Status { get; private set; }
    public TicketStatus PreviousState { get; private set; }
    public Guid RestaurantId { get; private set; }
    public List<TicketLineItem> LineItems { get; private set; }
    public DateTime ReadyBy { get; private set; }
    public DateTime AcceptTime { get; private set; }
    public DateTime PreparingTime { get; private set; }
    public DateTime PickedUpTime { get; private set; }
    public DateTime ReadyForPickupTime { get; private set; }

    public Ticket(Guid restaurantId, Guid id, TicketDetails details)
    {
        RestaurantId = restaurantId;
        Id = id;
        Status = TicketStatus.CREATE_PENDING;
        LineItems = details.LineItems;
    }

    public static (Ticket, List<IDomainEvent>) Create(Guid restaurantId, Guid id, TicketDetails details)
    {
        var ticket = new Ticket(restaurantId, id, details);
        return (ticket, [new TicketCreated(ticket.Id, details)]);
    }

    public List<IDomainEvent> ConfirmCreate()
    {
        switch (Status)
        {
            case TicketStatus.CREATE_PENDING:
                Status = TicketStatus.AWAITING_ACCEPTANCE;
                return [new TicketCreated(Id, new TicketDetails([]))];
            default:
                throw new UnsupportedStateTransitionException(Status);
        }
    }

    public List<IDomainEvent> CancelCreate()
    {
        throw new NotImplementedException();
    }

    public List<IDomainEvent> Accept(DateTime readyBy)
    {
        switch (Status)
        {
            case TicketStatus.AWAITING_ACCEPTANCE:
                AcceptTime = DateTime.Now;
                if (!AcceptTime.isBefore(readyBy))
                    throw new ArgumentException($"readyBy {readyBy} is not after now {AcceptTime}");
                ReadyBy = readyBy;
                return [new TicketAccepted(readyBy)];
            default:
                throw new UnsupportedStateTransitionException(Status);
        }
    }

    public List<IDomainEvent> Preparing()
    {
        switch (Status)
        {
            case TicketStatus.ACCEPTED:
                Status = TicketStatus.PREPARING;
                PreparingTime = DateTime.Now;
                return [new TicketPreparationStarted()];
            default:
                throw new UnsupportedStateTransitionException(Status);
        }
    }

    public List<IDomainEvent> ReadyForPickup()
    {
        switch (Status)
        {
            case TicketStatus.PREPARING:
                Status = TicketStatus.READY_FOR_PICKUP;
                ReadyForPickupTime = DateTime.Now;
                return [new TicketPreparationCompleted()];
            default:
                throw new UnsupportedStateTransitionException(Status);
        }
    }

    public List<IDomainEvent> PickedUp()
    {
        switch (Status)
        {
            case TicketStatus.READY_FOR_PICKUP:
                Status = TicketStatus.PICKED_UP;
                PickedUpTime = DateTime.Now;
                return [new TicketPickedUp()];
            default:
                throw new UnsupportedStateTransitionException(Status);
        }
    }

    public void ChangeLineItemQuantity()
    {
        switch (Status)
        {
            case TicketStatus.AWAITING_ACCEPTANCE:
                // TODO
                break;
            case TicketStatus.PREPARING:
                // TODO
                break;
            default:
                throw new UnsupportedStateTransitionException(Status);
        }

    }

    public List<IDomainEvent> Cancel()
    {
        switch (Status)
        {
            case TicketStatus.AWAITING_ACCEPTANCE:
            case TicketStatus.ACCEPTED:
                PreviousState = Status;
                Status = TicketStatus.CANCEL_PENDING;
                return [];
            default:
                throw new UnsupportedStateTransitionException(Status);
        }
    }

    public List<IDomainEvent> ConfirmCancel()
    {
        switch (Status)
        {
            case TicketStatus.CANCEL_PENDING:
                Status = TicketStatus.CANCELLED;
                return [new TicketCancelled()];
            default:
                throw new UnsupportedStateTransitionException(Status);

        }
    }

    public List<IDomainEvent> UndoCancel()
    {
        switch (Status)
        {
            case TicketStatus.CANCEL_PENDING:
                Status = PreviousState;
                return [];
            default:
                throw new UnsupportedStateTransitionException(Status);

        }
    }

    public List<IDomainEvent> BeginReviseOrder(List<RevisedOrderLineItem> revisedOrderLineItems)
    {
        switch (Status)
        {
            case TicketStatus.AWAITING_ACCEPTANCE:
            case TicketStatus.ACCEPTED:
                PreviousState = Status;
                Status = TicketStatus.REVISION_PENDING;
                return [];
            default:
                throw new UnsupportedStateTransitionException(Status);
        }
    }

    public List<IDomainEvent> UndoBeginReviseOrder()
    {
        switch (Status)
        {
            case TicketStatus.REVISION_PENDING:
                Status = PreviousState;
                return [];
            default:
                throw new UnsupportedStateTransitionException(Status);
        }
    }

    public List<IDomainEvent> ConfirmReviseTicket(List<RevisedOrderLineItem> revisedOrderLineItems)
    {
        switch (Status)
        {
            case TicketStatus.REVISION_PENDING:
                Status = PreviousState;
                return [new TicketRevised()];
            default:
                throw new UnsupportedStateTransitionException(Status);

        }
    }
}
