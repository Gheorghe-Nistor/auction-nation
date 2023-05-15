using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.Bid;
using FluentValidation;
using Microsoft.AspNetCore.Components.Forms;

namespace Cegeka.Auction.WebUI.Shared.Auction
{
    public class CreateAuctionItemRequest
    {
        public int Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public List<string> Images { get; set; } = new List<string>();
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public Category Category { get; set; }
        public float Lat { get; set; } = 0;
        public float Lon { get; set; } = 0;

        public decimal StartingBidAmount { get; set; } = 0;

        public int CurrencyId { get; set; }

        public decimal CurrentBidAmount { get; set; }

        public decimal BuyItNowPrice { get; set; }

        public decimal ReservePrice { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }

        public int Status { get; set; }

        public List<BidDTO> BiddingHistory { get; set; } = new List<BidDTO>();

        public CreateAuctionItemRequest(AuctionItemDTO newAuctionItem)
        {
            Id = newAuctionItem.Id;
            Title = newAuctionItem.Title;
            Description = newAuctionItem.Description;
            Images = newAuctionItem.Images;
            StartDate = newAuctionItem.StartDate;
            EndDate = newAuctionItem.EndDate;
            Category = newAuctionItem.Category;
            StartingBidAmount= newAuctionItem.StartingBidAmount;
            CurrencyId = newAuctionItem.CurrencyId;
            CurrentBidAmount = newAuctionItem.CurrentBidAmount;
            BuyItNowPrice = newAuctionItem.BuyItNowPrice;
            ReservePrice = newAuctionItem.ReservePrice;
            DeliveryMethod = newAuctionItem.DeliveryMethod;
            Status = newAuctionItem.Status;
            Lat = newAuctionItem.Lat;
            Lon = newAuctionItem.Lon;

            BiddingHistory = newAuctionItem.BiddingHistory;
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

            RuleFor(v => v.Images)
              .NotEmpty().WithMessage("This field is required.");
            RuleFor(v => v.Category)
                .Must(v => Enum.IsDefined(typeof(Category), v))
                .NotEmpty().WithMessage("This field is required.");

            RuleFor(v => v.StartingBidAmount)
                .NotNull().WithMessage("This field is required.");

            RuleFor(v => v.BuyItNowPrice)
                .NotNull().WithMessage("This field is required.")
                .GreaterThan(v => v.StartingBidAmount).WithMessage("Please increase the price.");

            RuleFor(v => v.ReservePrice)
                .NotNull().WithMessage("This field is required.")
                .GreaterThan(v => v.StartingBidAmount).WithMessage("Please increase the price."); 

            RuleFor(v => v.DeliveryMethod)
                .Must(v => Enum.IsDefined(typeof(DeliveryMethod),v))
                .NotEmpty().WithMessage("This field is required.");

            RuleFor(v => v.Status)
             .Must(v => Enum.IsDefined(typeof(Status), v));

            RuleFor(v => v.StartDate)
                .NotEmpty();
            
            RuleFor(v => v.EndDate)
                .NotEmpty()
                .GreaterThanOrEqualTo(v => v.StartDate);
        }
    }    
}

