using Marten;
using Marten.Events.Aggregation;
using Shiftly.Domain.Events.Organization;
using Shiftly.Domain.Entities;

namespace Shiftly.Domain.Projections.OrganizationMembers;

public class OrganizationMembersProjection : SingleStreamProjection<OrganizationMembers, Guid>
{
    public void Apply(OrganizationCreated created, OrganizationMembers organizationMembers, IQuerySession session)
    {
        if (created == null)
        {
            throw new ArgumentNullException(nameof(created), "OrganizationCreated event cannot be null.");
        }
        
        organizationMembers.Id = created.OrganizationId;
        organizationMembers.Members = new List<OrganizationMember>();
        
        var user = session.Query<User>().FirstOrDefault(u => u.Id == created.OwnerId);
        
        if (user == null)
            throw new Exception("User not found.");
            
        var member = new OrganizationMember
        {
            UserId = created.OwnerId,
            FullName = $"{user.FirstName} {user.LastName}".Trim()
        };
        organizationMembers.Members.Add(member);
    }

    public void Apply(UserAddedToOrganization added, OrganizationMembers organizationMembers, IQuerySession session)
    {
        if (added == null)
        {
            throw new ArgumentNullException(nameof(added), "UserAddedToOrganization event cannot be null.");
        }

        if (organizationMembers.Members.Any(m => m.UserId == added.UserId))
        {
            return;
        }

        var user = session.Query<User>().FirstOrDefault(u => u.Id == added.UserId);
        if (user != null)
        {
            var member = new OrganizationMember
            {
                UserId = added.UserId,
                FullName = $"{user.FirstName} {user.LastName}".Trim()
            };
            organizationMembers.Members.Add(member);
        }
    }

    public void Apply(UserRemovedFromOrganization removed, OrganizationMembers organizationMembers)
    {
        if (removed == null)
        {
            throw new ArgumentNullException(nameof(removed), "UserRemovedFromOrganization event cannot be null.");
        }

        organizationMembers.Members.RemoveAll(m => m.UserId == removed.UserId);
    }
}