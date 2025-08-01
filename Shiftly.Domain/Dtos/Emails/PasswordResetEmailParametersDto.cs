namespace Shiftly.Domain.Dtos.Emails;

public record PasswordResetEmailParametersDto(string Email, string ResetToken)
    : EmailBase(Email);