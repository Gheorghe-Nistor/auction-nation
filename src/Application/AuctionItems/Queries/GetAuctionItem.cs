using Cegeka.Auction.WebUI.Shared.Auction;

namespace Cegeka.Auction.Application.AuctionItems.Queries;

public record GetAuctionItemQuery(string Id) : IRequest<AuctionItemDetailsVM>;

public class GetAuctionItemQueryHandler : IRequestHandler<GetAuctionItemQuery, AuctionItemDetailsVM>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAuctionItemQueryHandler(
       IApplicationDbContext context,
       IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AuctionItemDetailsVM> Handle(
        GetAuctionItemQuery request,
        CancellationToken cancellationToken)
    {
        return new AuctionItemDetailsVM
        {
            Auction = await _context.AuctionItems
                .ProjectTo<AuctionItemDTO>(_mapper.ConfigurationProvider)
                .Where(a => a.Id == int.Parse(request.Id))
                .FirstOrDefaultAsync()
        };
    }
}
