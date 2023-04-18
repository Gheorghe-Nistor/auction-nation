using Cegeka.Auction.Application.AuctionItems.Queries;
using Cegeka.Auction.Application.Listings.Queries;
using Cegeka.Auction.WebUI.Shared.Listings;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Cegeka.Auction.WebUI.Server.Controllers;

[Route("api/[controller]")]
public class ListingsController : ApiControllerBase {
    // GET: api/listings
    [HttpGet]
    public async Task<ActionResult<ListingsVM>> GetListings([FromQuery] string? search)
    {
        ListingsQueryParams queryParams = new ListingsQueryParams
        {
            Search = search ?? String.Empty
        };

        /*
        if (Request.Query != null && Request.Body.CanRead)
        {
            queryParams = new ListingsQueryParams()
            {
                Search = GetStringFromQueryString("search"),
                Category = GetStringFromQueryString("category"),
                Status = GetStringFromQueryString("status"),
                DeliveryMethod = GetStringFromQueryString("delivery-method"),
                MinPrice = GetDecimalFromQueryString("min-price"),
                MaxPrice = GetDecimalFromQueryString("max-price"),
                MinDate = GetDateTimeFromQueryString("min-date"),
                MaxDate = GetDateTimeFromQueryString("max-date")
            };
        }
        */

        return await Mediator.Send(new GetListingsQuery(queryParams));
    }

    private string GetStringFromQueryString(string str)
    {
        return Request.Query[str].FirstOrDefault() ?? String.Empty;
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

