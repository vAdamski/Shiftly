using Shiftly.Domain.Events.Common;

namespace Shiftly.Domain.Events.Organization;

public record UserAddedToOrganization(Guid OrganizationId, Guid UserId) : OrganizationEvent(OrganizationId);

