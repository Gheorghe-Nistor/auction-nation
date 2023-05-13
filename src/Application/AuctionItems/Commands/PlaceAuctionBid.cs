using Cegeka.Auction.WebUI.Shared.Bid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Application.AuctionItems.Commands
{
    public record PlaceAuctionBidCommand(int auctionItemId, BidDTO bid) : IRequest;

    public class PlaceAuctionBidCommandHandler
        : AsyncRequestHandler<PlaceAuctionBidCommand>
    {
        private readonly IApplicationDbContext _context;

        public PlaceAuctionBidCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        protected override async Task Handle(PlaceAuctionBidCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.AuctionItems.FindAsync(request.auctionItemId);
           
            Guard.Against.NotFound(request.auctionItemId, entity);

            var maxBid = await _context.Bids
                .Where(b => b.AuctionItemId == request.auctionItemId)
                .OrderByDescending(b => b.Amount)
                .Select(b => b.Amount)
                .FirstOrDefaultAsync();

            decimal? currentBidAmount = request.bid.Amount;

            if(currentBidAmount != null && currentBidAmount < maxBid+1) 
            {
                return;
            }

            Bid newBid = new Bid {
                Amount = request.bid.Amount,
                CreatedBy = request.bid.CreatedBy,
                CreatedUtc = DateTime.UtcNow,
                CurrencyId= request.bid.CurrencyId
            };

            entity.CurrentBidAmount = newBid.Amount;
            entity.BiddingHistory.Add(newBid);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
