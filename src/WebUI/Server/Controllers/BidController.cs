using Cegeka.Auction.Application.Bids.Queries;
using Cegeka.Auction.WebUI.Shared.Bid;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Cegeka.Auction.WebUI.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BidController : ApiControllerBase
    {
        [HttpGet("createdBy/{userId}")]
        public async Task<ActionResult<BidVM>> GetBidByUserId(string userId)
        {
            return await Mediator.Send(new GetBidsQuery(userId));
        }
    }
}
