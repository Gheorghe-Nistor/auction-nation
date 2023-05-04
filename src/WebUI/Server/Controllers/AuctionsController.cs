using Azure.Core;
using Cegeka.Auction.Application.AuctionItems.Commands;
using Cegeka.Auction.Application.AuctionItems.Queries;
using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.Auction;
using Cegeka.Auction.WebUI.Shared.Bid;
using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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

    // GET: api/auctions/12345/won
    [HttpGet("{userId}/won")]
    public async Task<ActionResult<AuctionItemsVM>> GetWonAuctionsByUserId(string userId)
    {
        return await Mediator.Send(new GetWonAuctionItemsQuery(userId));
    }

    // GET: api/auctions/12345/created
    [HttpGet("{userId}/created")]
    public async Task<ActionResult<AuctionItemsVM>> GetCreatedAuctionsByUserId(string userId)
    {
        return await Mediator.Send(new GetCreatedAuctionItemsQuery(userId));
    }

    // GET: api/auctions/3/view
    [HttpGet("{id}/view")]
    public async Task<ActionResult<AuctionItemDetailsVM>> GetAuction(string id)
    {
        return await Mediator.Send(new GetAuctionItemQuery(id));
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
    public async Task<IActionResult> PutAuctionItem(int id, AuctionItemDTO updatedAuctionItem)
    {
        if (id != updatedAuctionItem.Id) return BadRequest();

        UpdateAuctionItemRequest request = new UpdateAuctionItemRequest(updatedAuctionItem);
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

    // POST: api/auction/1/buy/12345
    [HttpPost("{auctionItemId}/buy/{userId}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> BuyAuctionItem(int auctionItemId, string userId)
    {
        var response = await Mediator.Send(new GetAuctionItemQuery(auctionItemId.ToString()));

        if (response.Auction.CreatedBy == userId || response.Auction.WinningBidder != null || DateTime.Now > response.Auction.StartDate)
            return BadRequest();

        await Mediator.Send(new BuyAuctionItemCommand(auctionItemId, userId));

        return NoContent();
    }

    // POST: api/auction/12345/validate/1
    [HttpPost("{userId}/validate/{auctionItemId}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> ValidateAuctionItem(string userId, int auctionItemId)
    {
        var response = await Mediator.Send(new GetAuctionItemQuery(auctionItemId.ToString()));

        if (response == null || response.Auction.CreatedBy != userId || response.Auction.WinningBidder != null)
            return BadRequest();

        await Mediator.Send(new ValidateAuctionItemCommand(auctionItemId));

        return Ok();
    }

    // POST: api/auction/12345/bid
    [HttpPost("{auctionItemId}/bid")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesDefaultResponseType]
    public async Task<IActionResult> PlaceAuctionBid(int auctionItemId, BidDTO bid)
    {
        var response = await Mediator.Send(new GetAuctionItemQuery(auctionItemId.ToString()));

        if (response.Auction.CreatedBy == bid.CreatedBy || response.Auction.WinningBidder != null || response.Auction.Status != (int)Status.InProgress)
            return BadRequest();

        if (DateTime.Now < response.Auction.StartDate || DateTime.Now > response.Auction.EndDate)
            return BadRequest();

        await Mediator.Send(new PlaceAuctionBidCommand(auctionItemId, bid));

        return Ok();
    }
}
