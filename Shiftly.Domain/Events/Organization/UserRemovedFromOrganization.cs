using Shiftly.Domain.Events.Common;

namespace Shiftly.Domain.Events.Organization;

public class UserRemovedFromOrganization(Guid organizationId, Guid userId) : OrganizationEvent
{
	public Guid OrganizationId { get; } = organizationId;
	public Guid UserId { get; } = userId;

	public override Guid StreamId => OrganizationId;
}
