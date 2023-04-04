using Cegeka.Auction.Application.AuctionItems.Commands;
using Cegeka.Auction.Application.AuctionItems.Queries;
using Cegeka.Auction.WebUI.Shared.Auction;
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

    // TODO add /new path
    // POST: api/auctions
    [HttpPost]
    public async Task<ActionResult<int>> AddAuction(
        CreateAuctionItemRequest request)
    {
        return await Mediator.Send(new CreateAuctionItemCommand(request));
    }
}
