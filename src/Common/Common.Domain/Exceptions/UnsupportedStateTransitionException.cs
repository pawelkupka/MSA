namespace Common.Domain.Exceptions;

public class UnsupportedStateTransitionException : Exception
{
    public UnsupportedStateTransitionException(Enum state) : base($"current state: {state}")
    {
    }
}
