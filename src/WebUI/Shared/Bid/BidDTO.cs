using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.WebUI.Shared.Bid;

public class BidDTO
{
    public int Id { get; set; }
    public int AuctionItemId { get; set; }
    public string? CreatedBy { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedUtc { get; set; }
    public int CurrencyId { get; set; }

    public BidDTO(int id, int auctionItemId, string? createdBy, decimal amount, DateTime createdUtc, int currencyId)
    {
        Id = id;
        AuctionItemId = auctionItemId;
        CreatedBy = createdBy;
        Amount = amount;
        CreatedUtc = createdUtc;
        CurrencyId = currencyId;
    }

    public BidDTO()
    {

    }
}
