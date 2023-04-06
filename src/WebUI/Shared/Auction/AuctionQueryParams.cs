using Cegeka.Auction.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.WebUI.Shared.Auction;

public class AuctionQueryParams
{
    public string? Search { get; set; }

    public string? Category { get; set; }

    //[FromQuery(Name = "min-prince")]
    public decimal? MinPrice { get; set; }

    //[FromQuery(Name = "max-price")] 
    public decimal? MaxPrice { get; set; }

    public string? Status { get; set; }

    public string? DeliveryMethod { get; set; }

    public DateTime? MinDate { get; set; }

    public DateTime? MaxDate { get; set; }

    public AuctionQueryParams() : this(null, null, null, null, null, null, null, null)
    {
    }

    public AuctionQueryParams(string? search, string? category, decimal? minPrice, decimal? maxPrice, DateTime? minDate, DateTime? maxDate, string? status, string? deliveryMethod)
    {
        Search = search?.Trim();
        Category = category?.Trim();
        MinPrice = minPrice;
        MaxPrice = maxPrice;
        MinDate = minDate;
        MaxDate = maxDate;
        Status = status?.Trim();
        DeliveryMethod = deliveryMethod?.Trim();
    }
}
