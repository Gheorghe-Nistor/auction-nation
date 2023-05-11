using Cegeka.Auction.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.WebUI.Shared.Listings;

public class ListingsQueryParams
{
    public string Search { get; set; } = String.Empty;

    public int Category { get; set; } = 0;

    public string Status { get; set; } = String.Empty;

    public string DeliveryMethod { get; set; } = String.Empty;

    public decimal? MinPrice { get; set; } = null;

    public decimal? MaxPrice { get; set; } = null;

    public DateTime? MinDate { get; set; } = null;

    public DateTime? MaxDate { get; set; } = null;

    public ListingsQueryParams() { }

    public ListingsQueryParams(string search, int category, string status, string deliveryMethod, decimal? minPrice, decimal? maxPrice, DateTime? minDate, DateTime? maxDate)
    {
        Search = search.Trim();
        Category = category;
        Status = status.Trim();
        DeliveryMethod = deliveryMethod.Trim();
        MinPrice = minPrice;
        MaxPrice = maxPrice;
        MinDate = minDate;
        MaxDate = maxDate;
    }
}
