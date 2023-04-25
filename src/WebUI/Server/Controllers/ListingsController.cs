using Cegeka.Auction.Application.AuctionItems.Queries;
using Cegeka.Auction.Application.Listings.Queries;
using Cegeka.Auction.WebUI.Shared.Listings;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cegeka.Auction.WebUI.Server.Controllers;

[Route("api/[controller]")]
public class ListingsController : ApiControllerBase {
    // POST: api/listings
    [HttpPost]
    public async Task<ActionResult<ListingsVM>> PostListings([FromBody] ListingsQueryParams queryParams)
    {
        return await Mediator.Send(new GetListingsQuery(queryParams));
    }
}

