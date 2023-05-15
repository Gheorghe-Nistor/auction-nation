using Cegeka.Auction.WebUI.Shared.Bid;

namespace Cegeka.Auction.Application.Bids;

public class Mapping : Profile
{
    public Mapping()
    {
        CreateMap<Bid, BidDTO>()
               .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id));
    }
}
