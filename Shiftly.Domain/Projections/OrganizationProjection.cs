using Marten.Events.Aggregation;
using Shiftly.Domain.Entities;
using Shiftly.Domain.Events.Organization;

namespace Shiftly.Domain.Projections;

public class OrganizationProjection : SingleStreamProjection<Organization, Guid>
{
	public void Apply(OrganizationCreated created, Organization organization)
	{
		organization.Id = created.OrganizationId;
		organization.Name = created.Name;
		organization.OwnerId = created.OwnerId;
		organization.MemberIds = new List<Guid> { created.OwnerId };
	}

	public void Apply(UserAddedToOrganization added, Organization organization)
	{
		if (!organization.MemberIds.Contains(added.UserId))
		{
			organization.MemberIds.Add(added.UserId);
		}
	}
}


