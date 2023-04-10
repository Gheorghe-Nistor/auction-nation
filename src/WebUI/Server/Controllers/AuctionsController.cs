using Cegeka.Auction.Application.AuctionItems.Commands;
using Cegeka.Auction.Application.AuctionItems.Queries;
using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.Auction;
using Microsoft.AspNetCore.Mvc;

namespace Cegeka.Auction.WebUI.Server.Controllers;

[Route("api/[controller]")]
public class AuctionsController : ApiControllerBase
{
    // GET: api/auctions
    [HttpGet]
    public async Task<ActionResult<AuctionItemsVM>> GetAuctions(AuctionQueryParams? queryParams = null)
    {
        // This action method does not use the [FromQuery] attribute because the generated code
        // would make the parameters required, whereas we want to make them optional with default values.

        queryParams = queryParams ?? new AuctionQueryParams()
        {
            Search = Request.Query["search"].FirstOrDefault(),
            Category = Request.Query["category"].FirstOrDefault(),
            Status = Request.Query["status"].FirstOrDefault(),
            DeliveryMethod = Request.Query["deliveryMethod"].FirstOrDefault(),
            MinPrice = GetDecimalFromQueryString("min-price"),
            MaxPrice = GetDecimalFromQueryString("max-price"),
            MinDate = GetDateTimeFromQueryString("min-date"),
            MaxDate = GetDateTimeFromQueryString("max-date")
        };

        return await Mediator.Send(new GetAuctionItemsQuery(queryParams));
    }

    // GET: api/auctions/5
    [HttpGet("{id}")]
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

    private decimal? GetDecimalFromQueryString(string key)
    {
        decimal result;
        return decimal.TryParse(Request.Query[key].FirstOrDefault(), out result) ? result : null;
    }

    private DateTime? GetDateTimeFromQueryString(string key)
    {
        DateTime result;
        return DateTime.TryParse(Request.Query[key].FirstOrDefault(), out result) ? result : null;
    }
}