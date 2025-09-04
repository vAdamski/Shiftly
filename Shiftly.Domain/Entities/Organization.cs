namespace Shiftly.Domain.Entities;

public class Organization
{
	public Guid Id { get; set; }
	public string Name { get; set; } = string.Empty;
	public Guid OwnerId { get; set; }
	public List<Guid> MemberIds { get; set; } = new();
}


