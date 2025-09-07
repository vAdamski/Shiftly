using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shiftly.Application.Actions.OrganizationsActions.Commands.CreateOrganization;
using Shiftly.Application.Actions.OrganizationsActions.Commands.AddUserToOrganization;

namespace Shiftly.Api.Controllers;

[Route("api/organizations")]
public class OrganizationsController(ISender sender) : BaseApiController(sender)
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateOrganizationCommand command)
    {
        var result = await Sender.Send(command);

        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }

    [HttpPost("{organizationId}/members")]
    public async Task<IActionResult> AddMember([FromRoute] Guid organizationId, [FromBody] AddUserToOrganizationCommand command)
    {
        command.OrganizationId = organizationId;
        var result = await Sender.Send(command);

        return result.IsSuccess ? Ok(result.Value) : HandleFailure(result);
    }
}