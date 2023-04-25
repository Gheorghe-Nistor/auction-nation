using Cegeka.Auction.Domain.Events;
using Cegeka.Auction.WebUI.Shared.Bid;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Application.Bids.Commands
{
    public record CreateBidCommand(CreateBidRequest Item) : IRequest<int>;

    public class CreateBidCommandValidator : AbstractValidator<CreateBidCommand>
    {
        public CreateBidCommandValidator()
        {
            RuleFor(p => p.Item).SetValidator(new CreateBidRequestValidator());
        }
    }

    public class CreateBidCommandHandler
            : IRequestHandler<CreateBidCommand, int>
    {
        private readonly IApplicationDbContext _context;

        public CreateBidCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateBidCommand request,
            CancellationToken cancellationToken)
        {
            var entity = new Bid
            {
                Amount = request.Item.Amount,
                CreatedAt = DateTime.UtcNow
            };

            _context.Bids.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }

}
