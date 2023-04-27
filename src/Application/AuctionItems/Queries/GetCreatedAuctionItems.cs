using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.Auction;
using Cegeka.Auction.WebUI.Shared.Listings;

namespace Cegeka.Auction.Application.AuctionItems.Queries;

public record GetCreatedAuctionItemsQuery(string userId) : IRequest<AuctionItemsVM>;

public class GetCreatedAuctionItemsQueryHandler
       : IRequestHandler<GetCreatedAuctionItemsQuery, AuctionItemsVM>

{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetCreatedAuctionItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AuctionItemsVM> Handle(
        GetCreatedAuctionItemsQuery request,
        CancellationToken cancellationToken)
    {
        return new AuctionItemsVM
        {
            Auctions = await _context.AuctionItems
                .Where(a => a.CreatedBy == request.userId)
                .ProjectTo<AuctionItemDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }
}
