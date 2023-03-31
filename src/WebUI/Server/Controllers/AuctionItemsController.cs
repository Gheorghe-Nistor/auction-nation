using Cegeka.Auction.Application.AuctionItems.Queries;
using Cegeka.Auction.Application.TodoLists.Queries;
using Cegeka.Auction.WebUI.Shared.Auction;
using Cegeka.Auction.WebUI.Shared.TodoLists;
using Microsoft.AspNetCore.Mvc;

namespace Cegeka.Auction.WebUI.Server.Controllers;

public class AuctionItemsController : ApiControllerBase
{
    // GET: api/AuctionItems
    [HttpGet]
    public async Task<ActionResult<AuctionItemsVM>> GetAuctionItems([FromQuery] AuctionFilterDTO filter)
    {
        return await Mediator.Send(new GetAuctionItemsQuery(filter));
    }
}
