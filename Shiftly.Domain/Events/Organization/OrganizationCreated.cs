using Shiftly.Domain.Events.Common;

namespace Shiftly.Domain.Events.Organization;

public record OrganizationCreated(Guid OrganizationId, string Name, Guid OwnerId) : OrganizationEvent(OrganizationId);

