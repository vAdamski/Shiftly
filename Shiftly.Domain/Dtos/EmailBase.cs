namespace Shiftly.Domain.Dtos;

public class EmailBase(string emailDestination)
{
    public string Email { get; private set; } = emailDestination;
}