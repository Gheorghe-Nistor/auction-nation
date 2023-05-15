using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.Domain.Events;
using Cegeka.Auction.WebUI.Shared.Auction;
using Cegeka.Auction.WebUI.Shared.Bid;
using Cegeka.Auction.WebUI.Shared.TodoItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Cegeka.Auction.Application.AuctionItems.Commands
{
    public record UpdateAuctionItemCommand(UpdateAuctionItemRequest Item) : IRequest;

    public class UpdateAuctionItemCommandHandler
            : AsyncRequestHandler<UpdateAuctionItemCommand>
    {
        private readonly IApplicationDbContext _context;
        private readonly IMapper _mapper;
        public UpdateAuctionItemCommandHandler(IApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        protected override async Task Handle(UpdateAuctionItemCommand request,
                CancellationToken cancellationToken)
        {
            var entity = await _context.AuctionItems.FirstOrDefaultAsync(
                i => i.Id == request.Item.Id, cancellationToken);

            Guard.Against.NotFound(request.Item.Id, entity);

            entity!.Id = request.Item.Id;
            entity.Title = request.Item.Title;
            entity.Category = request.Item.Category;
            entity.Description = request.Item.Description;
            entity.Images = request.Item.Images;
            entity.StartingBidAmount = request.Item.StartingBidAmount;
            entity.CurrencyId = request.Item.CurrencyId;
            entity.CurrentBidAmount = request.Item.CurrentBidAmount;
            entity.BuyItNowPrice = request.Item.BuyItNowPrice;
            entity.ReservePrice = request.Item.ReservePrice;
            entity.DeliveryMethod = (DeliveryMethod)request.Item.DeliveryMethod;
            entity.Status = (Status)request.Item.Status;
            entity.StartDate = request.Item.StartDate;
            entity.EndDate = request.Item.EndDate;
            entity.Lat = request.Item.Lat;
            entity.Lon = request.Item.Lon;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
