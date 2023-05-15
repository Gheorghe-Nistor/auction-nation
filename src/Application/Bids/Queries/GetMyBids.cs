using Cegeka.Auction.WebUI.Shared.Bid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Application.Bids.Queries;

public record GetBidsQuery(string userId) : IRequest<BidVM>;

public class GeyBidsQueryHandler : IRequestHandler<GetBidsQuery, BidVM>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GeyBidsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<BidVM> Handle(GetBidsQuery request, CancellationToken cancellationToken)
    {
        return new BidVM
        {
            Bids = await _context.Bids
        .Where(a => a.CreatedBy == request.userId)
        .OrderByDescending(a => a.CreatedUtc)
        .Join(
            _context.AuctionItems,
            bid => bid.AuctionItemId,
            auction => auction.Id,
            (bid, auction) => new BidDTOAuctionTitle
            {
                Id = bid.Id,
                Amount = bid.Amount,
                AuctionItemId= auction.Id,
                CreatedUtc = bid.CreatedUtc,
                Title = auction.Title,
                CurrencyId = bid.CurrencyId
            }
        )
        .ToListAsync(cancellationToken)
        };
    }
}
