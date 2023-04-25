using Cegeka.Auction.Application.Bids.Commands;
using Cegeka.Auction.WebUI.Shared.Bid;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cegeka.Auction.WebUI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ApiControllerBase
    {
        // POST: api/bid/new
        [HttpPost("new")]
        public async Task<ActionResult<int>> AddAuction(BidDTO newBid)
        {
            CreateBidRequest request = new CreateBidRequest(newBid);

            return await Mediator.Send(new CreateBidCommand(request));
        }
    }
}
