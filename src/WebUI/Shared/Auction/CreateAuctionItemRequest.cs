using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.WebUI.Shared.Auction
{
    public class CreateAuctionItemRequest
    {
        public int ListId { get; set; }

        public string Title { get; set; } = string.Empty;
    }

    public class CreateAuctionItemRequestValidator
        : AbstractValidator<CreateAuctionItemRequest>
    {
        public CreateAuctionItemRequestValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(240)
                .NotEmpty();
        }
    }
}
