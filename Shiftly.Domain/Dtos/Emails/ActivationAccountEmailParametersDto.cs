using Shiftly.Domain.Common;

namespace Shiftly.Domain.Dtos.Emails;

public record ActivationAccountEmailParametersDto(string Email, Guid UserId, string ActivationToken, string FirstName)
    : EmailBase(Email);