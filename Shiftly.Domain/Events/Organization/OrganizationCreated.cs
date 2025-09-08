using Shiftly.Domain.Events.Common;

namespace Shiftly.Domain.Events.Organization;

public class OrganizationCreated(Guid organizationId, string name, Guid ownerId) : OrganizationEvent
{
	public Guid OrganizationId { get; } = organizationId;
	public string Name { get; } = name;
	public Guid OwnerId { get; } = ownerId;

	public override Guid StreamId => OrganizationId;
}


