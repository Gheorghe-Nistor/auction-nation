using Cegeka.Auction.Application.AuctionItems.Queries;
using Cegeka.Auction.Application.Users.Queries;
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

        var Auctions = await _context.AuctionItems
                .Where(a => string.IsNullOrEmpty(search) || a.Title.Contains(search) || a.Description.Contains(search))
                .Where(a => category == Category.None || a.Category.Equals(category))
                .Where(a => publicStatus == PublicStatus.None || (publicStatus == PublicStatus.Closed && (a.EndDate < DateTime.Now || a.Status == Status.Finished)) || (publicStatus == PublicStatus.Active && DateTime.Now < a.EndDate && (a.Status == Status.InProgress || a.Status == Status.AwaitingValidation)))
                .Where(a => minPrice == null || (a.CurrentBidAmount != 0 ? a.CurrentBidAmount >= minPrice : a.StartingBidAmount >= minPrice))
                .Where(a => maxPrice == null || (a.CurrentBidAmount != 0 ? a.CurrentBidAmount <= maxPrice : a.StartingBidAmount <= maxPrice))
                .ProjectTo<AuctionItemDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

        for(var i = 0; i < Auctions.Count; i++)
        {
            var auction = Auctions[i];

            if(auction.EndDate < DateTime.Now || auction.Status == 7)
            {
                Auctions[i].PublicStatus = PublicStatus.Closed;
            }
            else
            {
                Auctions[i].PublicStatus = PublicStatus.Active;
            }
        }

        Auctions.Sort((firstAuction, secondAuction) =>
        {
            if (firstAuction.PublicStatus == PublicStatus.Active && secondAuction.PublicStatus == PublicStatus.Closed)
                return -1;
            if (firstAuction.PublicStatus == PublicStatus.Closed && secondAuction.PublicStatus == PublicStatus.Active)
                return 1;

            return DateTime.Compare(firstAuction.StartDate, secondAuction.StartDate);
        });

        return new ListingsVM {
            QueryParams = queryParams,
            Auctions = Auctions       };
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
