using Shiftly.Domain.Events.Common;

namespace Shiftly.Domain.Events.Organization;

public record UserRemovedFromOrganization(Guid OrganizationId, Guid UserId) : OrganizationEvent(OrganizationId);

