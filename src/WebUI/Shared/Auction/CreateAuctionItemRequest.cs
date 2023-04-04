using Cegeka.Auction.Domain.Enums;
using FluentValidation;

namespace Cegeka.Auction.WebUI.Shared.Auction
{
    public class CreateAuctionItemRequest
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Images { get; set; } = new List<string>();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Category { get; set; } = string.Empty;

        public decimal StartingBidAmount { get; set; } = 0;

        public decimal CurrentBidAmount { get; set; }

        public decimal BuyItNowPrice { get; set; }

        public decimal ReservePrice { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }

        public Status Status { get; set; }

        public CreateAuctionItemRequest(AuctionItemDTO newAuctionItem)
        {
            Title = newAuctionItem.Title;
            Description = newAuctionItem.Description;
            Images = newAuctionItem.Images;
            StartDate = newAuctionItem.StartDate;
            EndDate = newAuctionItem.EndDate;
            Category = newAuctionItem.Category;
            StartingBidAmount= newAuctionItem.StartingBidAmount;
            CurrentBidAmount = newAuctionItem.CurrentBidAmount;
            BuyItNowPrice = newAuctionItem.BuyItNowPrice;
            ReservePrice = newAuctionItem.ReservePrice;
            DeliveryMethod = newAuctionItem.DeliveryMethod;
            Status = newAuctionItem.Status;
        }
    }

    public class CreateAuctionItemRequestValidator
        : AbstractValidator<CreateAuctionItemRequest>
    {
        public CreateAuctionItemRequestValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(100).WithMessage("Please make the title shorter.")
                .NotEmpty().WithMessage("This field is required.");

            RuleFor(v => v.Description)
                .MaximumLength(500).WithMessage("Please make the description shorter.")
                .NotEmpty().WithMessage("This field is required.");

            RuleFor(v => v.Category) 
                .MaximumLength(50).WithMessage("Please make the category shorter.")
                .NotEmpty().WithMessage("This field is required.");

            RuleFor(v => v.StartingBidAmount)
                .NotNull();

            RuleFor(v => v.BuyItNowPrice)
                .GreaterThan(v => v.StartingBidAmount).WithMessage("Please increase the price.");

            RuleFor(v => v.ReservePrice)
                .GreaterThan(v => v.StartingBidAmount).WithMessage("Please increase the price."); 

            RuleFor(v => v.DeliveryMethod)
                .NotEmpty().WithMessage("This field is required.");

            RuleFor(v => v.EndDate)
                .GreaterThanOrEqualTo(v => v.StartDate);

            RuleFor(v => v.Images)
                .NotEmpty().WithMessage("Please add at least one picture.");
        }
    }    
}

