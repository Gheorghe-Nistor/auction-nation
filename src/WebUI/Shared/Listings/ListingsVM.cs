using Cegeka.Auction.WebUI.Shared.Auction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.WebUI.Shared.Listings;

public class ListingsVM
{
    public List<AuctionItemDTO> Auctions { get; set; } = new List<AuctionItemDTO>();

    public ListingsQueryParams QueryParams { get; set; } = new ListingsQueryParams();
}
