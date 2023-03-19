using Microsoft.AspNetCore.Mvc;
using Cegeka.Auction.Application.TodoLists.Commands;
using Cegeka.Auction.Application.TodoLists.Queries;
using Cegeka.Auction.WebUI.Shared.TodoLists;

namespace Cegeka.Auction.WebUI.Server.Controllers;

public class TodoListsController : ApiControllerBase
{
    // GET: api/TodoLists
    [HttpGet]
    public async Task<ActionResult<TodosVm>> GetTodoLists()
    {
        return await Mediator.Send(new GetTodoListsQuery());
    }

    // POST: api/TodoLists
    [HttpPost]
    public async Task<ActionResult<int>> PostTodoList(
        CreateTodoListRequest request)
    {
        return await Mediator.Send(new CreateTodoListCommand(request));
    }

    // PUT: api/TodoLists/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> PutTodoList(int id,
        UpdateTodoListRequest request)
    {
        if (id != request.Id) return BadRequest();

        await Mediator.Send(new UpdateTodoListCommand(request));

        return NoContent();
    }

    // DELETE: api/TodoLists/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteTodoList(int id)
    {
        await Mediator.Send(new DeleteTodoListCommand(id));

        return NoContent();
    }
}
