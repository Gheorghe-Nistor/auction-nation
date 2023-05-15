using Cegeka.Auction.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Application.AuctionItems.Commands
{
    public record ValidateAuctionItemCommand(int auctionItemId) : IRequest;

    public class ValidateAuctionItemCommandHandler
        : AsyncRequestHandler<ValidateAuctionItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public ValidateAuctionItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        protected override async Task Handle(ValidateAuctionItemCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.AuctionItems.FindAsync(request.auctionItemId);
           
            Guard.Against.NotFound(request.auctionItemId, entity);

            // Primeam eroare: Could not deserialize the response body stream as Microsoft.AspNetCore.Mvc.ProblemDetails
            // entity.WinningBidder = entity.BiddingHistory.Last().CreatedBy;

            entity.WinningBidder = await _context.Bids
                .Where(b => b.AuctionItemId == request.auctionItemId)
                .OrderByDescending(b => b.Amount)
                .Select(b => b.CreatedBy)
                .FirstOrDefaultAsync();

            entity.Status = Domain.Enums.Status.Finished;

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
