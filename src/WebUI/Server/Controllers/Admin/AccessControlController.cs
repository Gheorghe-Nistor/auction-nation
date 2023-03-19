using Microsoft.AspNetCore.Mvc;
using Cegeka.Auction.Application.AccessControl.Commands;
using Cegeka.Auction.Application.AccessControl.Queries;
using Cegeka.Auction.WebUI.Shared.AccessControl;
using Cegeka.Auction.WebUI.Shared.Authorization;

namespace Cegeka.Auction.WebUI.Server.Controllers.Admin;

[Route("api/Admin/[controller]")]
public class AccessControlController : ApiControllerBase
{
    [HttpGet]
    [Authorize(Permissions.ViewAccessControl)]
    public async Task<ActionResult<AccessControlVm>> GetConfiguration()
    {
        return await Mediator.Send(new GetAccessControlQuery());
    }

    [HttpPut]
    [Authorize(Permissions.ConfigureAccessControl)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> UpdateConfiguration(RoleDto updatedRole)
    {
        await Mediator.Send(new UpdateAccessControlCommand(updatedRole.Id, updatedRole.Permissions));

        return NoContent();
    }
}
