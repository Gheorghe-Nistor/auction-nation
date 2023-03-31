using Cegeka.Auction.WebUI.Shared.Auction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Application.AuctionItems.Queries;


public record GetAuctionItemsQuery(AuctionFilterDTO Filter) : IRequest<AuctionItemsVM>;

public class GetAuctionItemsQueryHandler
       : IRequestHandler<GetAuctionItemsQuery, AuctionItemsVM>

{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetAuctionItemsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<AuctionItemsVM> Handle(
        GetAuctionItemsQuery request,
        CancellationToken cancellationToken)
    {
        string? search = request.Filter.Search?.Trim();
        string? category = request.Filter.Search?.Trim();
        string? status = request.Filter.Status?.Trim();

        decimal? minAmount = request.Filter.MinAmount;
        decimal? maxAmount = request.Filter.MaxAmount;

        return new AuctionItemsVM
        {
            Auctions = await _context.AuctionItems
                .Where(a => string.IsNullOrEmpty(search) || a.Title.Contains(search) || a.Description.Contains(search))
                // Category not implemented .Where(a => string.IsNullOrEmpty(category) || a.Category.CategoryName.Equals(category))
                .Where(a => string.IsNullOrEmpty(status) || a.Status.Equals(status))
                .Where(a => minAmount == null || (a.CurrentBidAmount != null ? a.CurrentBidAmount >= minAmount : a.StartingBidAmount >= minAmount))
                .Where(a => maxAmount == null || (a.CurrentBidAmount != null ? a.CurrentBidAmount <= maxAmount : a.StartingBidAmount <= maxAmount))
                .ProjectTo<AuctionItemDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }
}
