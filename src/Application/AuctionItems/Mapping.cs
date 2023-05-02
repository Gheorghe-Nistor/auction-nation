using Cegeka.Auction.WebUI.Shared.Auction;

namespace Cegeka.Auction.Application.AuctionItems;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<AuctionItem, AuctionItemDTO>();


    }
}
