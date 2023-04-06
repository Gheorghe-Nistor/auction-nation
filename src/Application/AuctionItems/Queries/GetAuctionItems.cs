using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.Auction;

namespace Cegeka.Auction.Application.AuctionItems.Queries;

public record GetAuctionItemsQuery(AuctionQueryParams? QueryParams = null) : IRequest<AuctionItemsVM>;

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
        AuctionQueryParams queryParams = request.QueryParams ?? new AuctionQueryParams();

        string? search = queryParams?.Search;
        string? category = queryParams?.Category;
        decimal? minPrice = queryParams?.MinPrice;
        decimal? maxPrice = queryParams?.MaxPrice;
        
        DateTime? minDate = queryParams?.MinDate;
        DateTime? maxDate = queryParams?.MaxDate;

        Status status = Enum.TryParse(queryParams.Status, out Status statusParsedSuccessfully) ? statusParsedSuccessfully : Status.None;
        DeliveryMethod deliveryMethod = Enum.TryParse(queryParams.DeliveryMethod, out DeliveryMethod deliveryMethodParsedSuccessfully) ? deliveryMethodParsedSuccessfully : DeliveryMethod.None;

        return new AuctionItemsVM
        {
            Auctions = await _context.AuctionItems
                .Where(a => string.IsNullOrEmpty(search) || a.Title.Contains(search) || a.Description.Contains(search))
                .Where(a => string.IsNullOrEmpty(category) || a.Category.Equals(category))
                .Where(a => minPrice == null || (a.CurrentBidAmount != 0 ? a.CurrentBidAmount >= minPrice : a.StartingBidAmount >= minPrice))
                .Where(a => maxPrice == null || (a.CurrentBidAmount != 0 ? a.CurrentBidAmount <= maxPrice : a.StartingBidAmount <= maxPrice))
                .Where(a => minDate == null || minDate >= a.StartDate && minDate <= a.EndDate)
                .Where(a => maxDate == null || maxDate <= a.EndDate)
                .Where(a => status == Status.None || a.Status == status)
                .Where(a => deliveryMethod == DeliveryMethod.None || a.DeliveryMethod == deliveryMethod)
                .ProjectTo<AuctionItemDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }
}
