using Cegeka.Auction.Application.AuctionItems.Queries;
using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.Auction;
using Cegeka.Auction.WebUI.Shared.Listings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Application.Listings.Queries;

public record GetListingsQuery(ListingsQueryParams? QueryParams = null) : IRequest<ListingsVM>;

public class GetListingsQueryHandler
       : IRequestHandler<GetListingsQuery, ListingsVM>

{
    private readonly IApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetListingsQueryHandler(IApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ListingsVM> Handle(
        GetListingsQuery request,
        CancellationToken cancellationToken)
    {
        ListingsQueryParams queryParams = request.QueryParams ?? new ListingsQueryParams();

        string search = queryParams.Search;
        Category category = queryParams.Category;

        PublicStatus publicStatus = queryParams.PublicStatus;

        decimal? minPrice = queryParams?.MinPrice;
        decimal? maxPrice = queryParams?.MaxPrice;

        DateTime? minDate = queryParams?.MinDate;
        DateTime? maxDate = queryParams?.MaxDate;

        DeliveryMethod deliveryMethod = ParseEnum(queryParams?.DeliveryMethod, DeliveryMethod.None);

        return new ListingsVM {
            QueryParams = queryParams,
            Auctions = await _context.AuctionItems
                .Where(a => string.IsNullOrEmpty(search) || a.Title.Contains(search) || a.Description.Contains(search))
                .Where(a => category == Category.None || a.Category.Equals(category))
                .Where(a => publicStatus == PublicStatus.None || (publicStatus == PublicStatus.Closed && (a.EndDate < DateTime.Now || a.Status == Status.Finished)) || (publicStatus == PublicStatus.Active && DateTime.Now < a.EndDate && (a.Status == Status.InProgress || a.Status == Status.AwaitingValidation)))
                .Where(a => minPrice == null || (a.CurrentBidAmount != 0 ? a.CurrentBidAmount >= minPrice : a.StartingBidAmount >= minPrice))
                .Where(a => maxPrice == null || (a.CurrentBidAmount != 0 ? a.CurrentBidAmount <= maxPrice : a.StartingBidAmount <= maxPrice))
                .Where(a => minDate == null || minDate <= a.StartDate || minDate <= a.EndDate)
                .Where(a => maxDate == null || a.StartDate <= a.EndDate || a.EndDate <= maxDate)
                .Where(a => deliveryMethod == DeliveryMethod.None || a.DeliveryMethod == deliveryMethod)
                .OrderBy(a => a.StartDate)
                .ProjectTo<AuctionItemDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken)
        };
    }

    private T ParseEnum<T>(string? value, T defaultValue) where T : struct, Enum
    {
        if (Enum.TryParse<T>(value, true, out var result))
        {
            return result;
        }
        return defaultValue;
    }
}
