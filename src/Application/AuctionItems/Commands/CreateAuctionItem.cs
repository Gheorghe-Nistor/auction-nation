using Cegeka.Auction.Application.TodoItems.Commands;
using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.Domain.Events;
using Cegeka.Auction.WebUI.Shared.Auction;
using Cegeka.Auction.WebUI.Shared.TodoItems;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            CurrentBidAmount = request.Item.CurrentBidAmount,
            StartDate = DateTime.Now,
            EndDate = request.Item.EndDate,
            BuyItNowPrice = request.Item.BuyItNowPrice,
            ReservePrice = request.Item.ReservePrice,
            DeliveryMethod = (DeliveryMethod)request.Item.DeliveryMethod,
            Status = Status.New
        };

        _context.AuctionItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}

