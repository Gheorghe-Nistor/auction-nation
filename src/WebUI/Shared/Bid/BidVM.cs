using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.WebUI.Shared.Bid;

public class BidVM
{
    public List<BidDTOAuctionTitle> Bids { get; set; } = new List<BidDTOAuctionTitle>();
    public string CreatedBy { get; set; }

    public decimal Amount { get; set; }

    public DateTime CreatedUtc { get; set; }

    public int CurrencyId { get; set; }

    public BidVM(string createdBy, decimal amount, DateTime createdUtc, int currency)
    {
        CreatedBy = createdBy;
        Amount = amount;
        CreatedUtc = createdUtc;
        CurrencyId = currency;
    }
   

    public BidVM()
    {

    }

    public BidVM(List<BidDTOAuctionTitle> bidDTOs)
    {
        Bids = bidDTOs;
    }
}
