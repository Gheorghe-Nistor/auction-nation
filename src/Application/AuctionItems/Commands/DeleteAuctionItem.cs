using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Application.AuctionItems.Commands
{
    public record DeleteAuctionItemCommand(int Id) : IRequest;

    public class DeleteAuctionItemCommandHandler
        : AsyncRequestHandler<DeleteAuctionItemCommand>
    {
        private readonly IApplicationDbContext _context;

        public DeleteAuctionItemCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        protected override async Task Handle(DeleteAuctionItemCommand request,
            CancellationToken cancellationToken)
        {
            var entity = await _context.AuctionItems.FindAsync(request.Id);

            Guard.Against.NotFound(request.Id, entity);

            _context.AuctionItems.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
