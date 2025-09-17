namespace Shiftly.Domain.Events.Common;

public abstract record EventQueue
{
    public abstract string Serialize();
}