using Cegeka.Auction.Application.AuctionItems.Commands;
using Cegeka.Auction.Application.AuctionItems.Queries;
using Cegeka.Auction.Application.TodoItems.Commands;
using Cegeka.Auction.WebUI.Shared.Auction;
using Cegeka.Auction.WebUI.Shared.TodoItems;
using Microsoft.AspNetCore.Mvc;

namespace Cegeka.Auction.WebUI.Server.Controllers;

[Route("api/[controller]")]
public class AuctionsController : ApiControllerBase
{
    // GET: api/auctions
    [HttpGet]
    public async Task<ActionResult<AuctionItemsVM>> GetAuctions()
    {
        return await Mediator.Send(new GetAuctionItemsQuery());
    }

    // POST: api/auctions/new
    [HttpPost("new")]
    public async Task<ActionResult<int>> AddAuction(AuctionItemDTO newAuctionItem)
    {
        CreateAuctionItemRequest request = new CreateAuctionItemRequest(newAuctionItem);
        return await Mediator.Send(new CreateAuctionItemCommand(request));
    }

    // PUT: api/auctions/5
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> PutAuctionItem(int id,
        UpdateAuctionItemRequest request)
    {
        if (id != request.Id) return BadRequest();

        await Mediator.Send(new UpdateAuctionItemCommand(request));

        return NoContent();
    }

    // DELETE: api/auctions/5
    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> DeleteAuctionItem(int id)
    {
        await Mediator.Send(new DeleteAuctionItemCommand(id));

        return NoContent();
    }
}
