namespace Shiftly.Domain.Dtos;

public class ActivationAccountEmailParametersDto(
    string emailDestination,
    Guid userId,
    Guid activationToken,
    string firstName) : EmailBase(emailDestination)
{
    public Guid UserId { get; private set; } = userId;
    public Guid ActivationToken { get; private set; } = activationToken;
    public string FirstName { get; private set; } = firstName;
}