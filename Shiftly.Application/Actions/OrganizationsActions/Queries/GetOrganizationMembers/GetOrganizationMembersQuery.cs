using Marten;
using Shiftly.Application.Common.Abstraction.Messaging;
using Shiftly.Domain.Common;
using Shiftly.Domain.Projections.OrganizationMembers;

namespace Shiftly.Application.Actions.OrganizationsActions.Queries.GetOrganizationMembers;

public class GetOrganizationMembersQuery : IQuery<GetOrganizationMembersQueryResponse>
{
    public Guid OrganizationId { get; set; }
}

public class
    GetOrganizationMembersQueryHandler(IQuerySession session) : IQueryHandler<GetOrganizationMembersQuery, GetOrganizationMembersQueryResponse>
{
    public async Task<Result<GetOrganizationMembersQueryResponse>> Handle(GetOrganizationMembersQuery request,
        CancellationToken cancellationToken)
    {
        var members = await session.Events.AggregateStreamAsync<OrganizationMembers>(request.OrganizationId, token: cancellationToken);
        
        if (members == null)
        {
            throw new Exception("Organization not found.");
        }
        
        return Result.Success(new GetOrganizationMembersQueryResponse
        {
            Members = members.Members.Select(x => new OrganizationMemberDto(x.UserId, x.FullName)).ToList()
        });
    }
}

public class GetOrganizationMembersQueryResponse
{
    public List<OrganizationMemberDto> Members { get; set; } = new();
}

public record OrganizationMemberDto(Guid UserId, string FullName);