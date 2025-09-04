using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shiftly.Application.Actions.OrganizationsActions.Commands.CreateOrganization;

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
}