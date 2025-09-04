using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Shiftly.Api.Controllers;

[Route("api/organizations")]
public class OrganizationsController(ISender sender) : BaseApiController(sender)
{
    
}