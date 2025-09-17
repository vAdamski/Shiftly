namespace Shiftly.Domain.Projections.OrganizationMembers;

public class OrganizationMembers
{
    public Guid Id { get; set; }
    public List<OrganizationMember> Members { get; set; } = new();
}