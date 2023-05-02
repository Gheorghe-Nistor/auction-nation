using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.Auction;
using Cegeka.Auction.WebUI.Shared.Listings;

namespace Cegeka.Auction.Application.AuctionItems.Queries;

public record GetWonAuctionItemsQuery(string userId) : IRequest<AuctionItemsVM>;

public class GetWonAuctionItemsQueryHandler
       : IRequestHandler<GetWonAuctionItemsQuery, AuctionItemsVM>

{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetWonAuctionItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AuctionItemsVM> Handle(
        GetWonAuctionItemsQuery request,
        CancellationToken cancellationToken)
    {
        return new AuctionItemsVM
        {
            Auctions = await _context.AuctionItems
                .Where(a => a.WinningBidder == request.userId)
                .ProjectTo<AuctionItemDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }
}
