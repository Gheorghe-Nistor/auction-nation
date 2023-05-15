using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.Auction;

namespace Cegeka.Auction.Application.AuctionItems.Commands;

public record CreateAuctionItemCommand(CreateAuctionItemRequest Item) : IRequest<int>;

public class CreateAuctionItemCommandValidator : AbstractValidator<CreateAuctionItemCommand>
{
    public CreateAuctionItemCommandValidator()
    {
        RuleFor(p => p.Item).SetValidator(new CreateAuctionItemRequestValidator());
    }
}

public class CreateAuctionItemCommandHandler
        : IRequestHandler<CreateAuctionItemCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateAuctionItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateAuctionItemCommand request,
        CancellationToken cancellationToken)
    {
        var entity = new AuctionItem
        {
            Title = request.Item.Title,
            Category = request.Item.Category,
            Description = request.Item.Description,
            Images = request.Item.Images,
            StartingBidAmount = request.Item.StartingBidAmount,
            CurrencyId = request.Item.CurrencyId,
            CurrentBidAmount = request.Item.CurrentBidAmount,
            StartDate = request.Item.StartDate,
            EndDate = request.Item.EndDate,
            BuyItNowPrice = request.Item.BuyItNowPrice,
            ReservePrice = request.Item.ReservePrice,
            DeliveryMethod = request.Item.DeliveryMethod,
            Status = Status.New,
            BiddingHistory = new List<Bid>()
        };

        // TO DO
        entity.Status = Status.InProgress;

        _context.AuctionItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
