using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.Auction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Application.AuctionItems.Queries;

public record GetAuctionItemsQuery : IRequest<AuctionItemsVM>;

public class GetAuctionItemsQueryHandler : IRequestHandler<GetAuctionItemsQuery, AuctionItemsVM>
{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAuctionItemsQueryHandler(
        IApplicationDbContext context,
        IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AuctionItemsVM> Handle(
        GetAuctionItemsQuery request,
        CancellationToken cancellationToken)
    {
        return new AuctionItemsVM
        {
            Auctions = await _context.AuctionItems
                .ProjectTo<AuctionItemDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }
}
