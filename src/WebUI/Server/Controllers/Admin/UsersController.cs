using Microsoft.AspNetCore.Mvc;
using Cegeka.Auction.Application.Users.Commands;
using Cegeka.Auction.Application.Users.Queries;
using Cegeka.Auction.WebUI.Shared.AccessControl;
using Cegeka.Auction.WebUI.Shared.Authorization;

namespace Cegeka.Auction.WebUI.Server.Controllers.Admin;

[Route("api/Admin/[controller]")]
public class UsersController : ApiControllerBase
{
    // GET: api/Admin/Users
    [HttpGet]
    [Authorize(Permissions.ViewUsers | Permissions.ManageUsers)]
    public async Task<ActionResult<UsersVm>> GetUsers()
    {
        return await Mediator.Send(new GetUsersQuery());
    }

    // GET: api/Admin/Users/5
    [HttpGet("{id}")]
    [Authorize(Permissions.ViewUsers)]
    public async Task<ActionResult<UserDetailsVm>> GetUser(string id)
    {
        return await Mediator.Send(new GetUserQuery(id));
    }

    // GET: api/Admin/Users/username/admin@localhost
    [HttpGet("username/{userName}")]
    //[Authorize(Permissions.ViewUsers)]
    public async Task<ActionResult<string>> GetUserIdByUserName(string userName)
    {
        return await Mediator.Send(new GetUserIdByUsernameQuery(userName));
    }

    // PUT: api/Admin/Users/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Authorize(Permissions.ManageUsers)]
    public async Task<IActionResult> PutUser(string id, UserDto updatedUser)
    {
        if (id != updatedUser.Id) return BadRequest();

        await Mediator.Send(new UpdateUserCommand(updatedUser));

        return NoContent();
    }
}
