using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Application.AuctionItems.Commands
{
    public record BuyAuctionItemCommand(int auctionItemId, string userId) : IRequest;

    public class BuyAuctionItemCommandHandler
        : AsyncRequestHandler<BuyAuctionItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public BuyAuctionItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        protected override async Task Handle(BuyAuctionItemCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.AuctionItems.FindAsync(request.auctionItemId);
           
            Guard.Against.NotFound(request.auctionItemId, entity);

            entity.WinningBidder = request.userId;
            entity.Status = Domain.Enums.Status.Finished;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
