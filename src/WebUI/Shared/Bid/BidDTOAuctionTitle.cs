using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.WebUI.Shared.Bid
{
    public class BidDTOAuctionTitle
    {

        public int Id { get; set; }
        public int AuctionItemId { get; set; }
        public string? CreatedBy { get; set; }
        public decimal Amount { get; set; }

        public int CurrencyId { get; set; }
        public DateTime CreatedUtc { get; set; }

        public string Title { get; set; }

        public BidDTOAuctionTitle(int id, int auctionItemId, string? createdBy, decimal amount, DateTime createdUtc, int currency)
        {
            Id = id;
            AuctionItemId = auctionItemId;
            CreatedBy = createdBy;
            Amount = amount;
            CurrencyId = currency;
            CreatedUtc = createdUtc;
        }

        public BidDTOAuctionTitle()
        {

        }
    }
}
