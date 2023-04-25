using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.WebUI.Shared.Bid;

public class BidDTO
{
    public Guid Id { get; set; }
    public int ItemId { get; set; }
    public string? CreatedBy { get; set; }
    public decimal Amount { get; set; }
    public DateTime CreatedUtc { get; set; }
}
