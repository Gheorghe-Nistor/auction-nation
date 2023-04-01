using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.WebUI.Shared.Auction;
public class AuctionItemsVM
{
    // TODO: add PriorityLevels list

    public List<AuctionItemDTO> Auctions { get; set; } = new List<AuctionItemDTO>();

    public AuctionItemsVM() { }

    public AuctionItemsVM(List<AuctionItemDTO> auctions)
    {
        Auctions = auctions;
    }
}
