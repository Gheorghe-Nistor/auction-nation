using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.WebUI.Shared.Auction;

public class AuctionFilterDTO
{
    public string? Search { get; set; }
    
    public string? Category { get; set; } 
    
    public string? Status { get; set; }

    //[FromQuery(Name = "min-amount")]
    public decimal? MinAmount { get; set; }
    
    //[FromQuery(Name = "max-amount")] 
    public decimal? MaxAmount { get; set; }
}
