using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.Auction;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.WebUI.Shared.Bid
{
    public class CreateBidRequest
    {
        public decimal Amount { get; set; }
        public DateTime CreatedUtc { get; set; }

        public int ItemId { get; set; }

        public CreateBidRequest(BidDTO newBid)
        {
            ItemId = newBid.ItemId;
            Amount = newBid.Amount;
            CreatedUtc = newBid.CreatedUtc;
        }
    }

    public class CreateBidRequestValidator
        : AbstractValidator<CreateBidRequest>
    {
        public CreateBidRequestValidator()
        {
            RuleFor(x => x.Amount)
                .NotEmpty();
        }
    }
}
