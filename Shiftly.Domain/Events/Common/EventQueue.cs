namespace Shiftly.Domain.Events.Common;

public abstract class EventQueue
{
    public abstract string Serialize();
}