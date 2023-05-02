using Cegeka.Auction.WebUI.Shared.Bid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Application.Bids.Queries;

public record GetMaxBidQuery : IRequest<BidDTO>;

public class GetMaxBidQueryHandler : IRequestHandler<GetMaxBidQuery, BidDTO>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetMaxBidQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<decimal> GetMaxBidAsync(CancellationToken cancellationToken)
    {
        var max = await _context.Bids.MaxAsync(b => b.Amount, cancellationToken);

        if(max == null)
        {
            return 0;
        }

        return max;
    }


    public async Task<BidDTO> Handle(GetMaxBidQuery request, CancellationToken cancellationToken)
    {
        var maxBid = await GetMaxBidAsync(cancellationToken);
        var bid = _mapper.Map<BidDTO>(maxBid);
        return bid;
    }
}
