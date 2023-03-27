using Cegeka.Auction.WebUI.Shared.Bid;
using Cegeka.Auction.WebUI.Shared.TodoLists;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.WebUI.Shared.Auction
{
    public class UpdateAuctionItemRequest
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public int StartingBidAmount { get; set; }
        public int CurrentBidAmount { get; set; }
        public DateTime EndTime { get; set; }
        public string ShippingDetails { get; set; } = string.Empty;
    }
    public class UpdateAuctionItemRequestValidator
    : AbstractValidator<UpdateTodoListRequest>
    {
        public UpdateAuctionItemRequestValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(240)
                .NotEmpty();
        }
    }
}
