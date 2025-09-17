namespace Shiftly.Domain.Projections.OrganizationMembers;

public class OrganizationMember
{
    public Guid UserId { get; set; }
    public string FullName { get; set; } = string.Empty;
}