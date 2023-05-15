using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.Bid;
using FluentValidation;

namespace Cegeka.Auction.WebUI.Shared.Auction
{
    public class UpdateAuctionItemRequest
    {
        public int Id { get; set; }

        public string Title { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public float Lat { get; set; } = 0;
        public float Lon { get; set; } = 0;

        public List<string> Images { get; set; }

        public Category Category { get; set; }

        public decimal StartingBidAmount { get; set; } = 0;

        public int CurrencyId { get; set; }

        public decimal CurrentBidAmount { get; set; }

        public decimal BuyItNowPrice { get; set; }

        public decimal ReservePrice { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DeliveryMethod DeliveryMethod { get; set; }
        public int Status { get; set; }

        public List<BidDTO> BiddingHistory { get; set; } = new List<BidDTO>();

        public UpdateAuctionItemRequest() { }

        public UpdateAuctionItemRequest(AuctionItemDTO updatedAuctionItem)
        {
            Id = updatedAuctionItem.Id;
            Title = updatedAuctionItem.Title;
            Description = updatedAuctionItem.Description;
            Images = updatedAuctionItem.Images;
            StartDate = updatedAuctionItem.StartDate;
            EndDate = updatedAuctionItem.EndDate;
            Category = updatedAuctionItem.Category;
            StartingBidAmount = updatedAuctionItem.StartingBidAmount;
            CurrencyId = updatedAuctionItem.CurrencyId;
            CurrentBidAmount = updatedAuctionItem.CurrentBidAmount;
            BuyItNowPrice = updatedAuctionItem.BuyItNowPrice;
            ReservePrice = updatedAuctionItem.ReservePrice;
            DeliveryMethod = updatedAuctionItem.DeliveryMethod;
            Status = updatedAuctionItem.Status;
            Lat = updatedAuctionItem.Lat;
            Lon = updatedAuctionItem.Lon;
            BiddingHistory = updatedAuctionItem.BiddingHistory;
        }
    }
    public class UpdateAuctionItemRequestValidator
    : AbstractValidator<UpdateAuctionItemRequest>
    {
        public UpdateAuctionItemRequestValidator()
        {
            RuleFor(v => v.Title)
                .MaximumLength(100)
                .NotEmpty().WithMessage("This field is required."); ;

            RuleFor(v => v.Description)
                .MaximumLength(500)
                .NotEmpty().WithMessage("This field is required."); ;

            RuleFor(v => v.Images)
                .NotEmpty().WithMessage("This field is required."); ;

            RuleFor(v => v.Category)
                .Must(v => Enum.IsDefined(typeof(Category), v))
                .NotEmpty().WithMessage("This field is required."); ;

            RuleFor(v => v.StartingBidAmount)
                .NotNull();

            RuleFor(v => v.BuyItNowPrice)
                .GreaterThan(v => v.StartingBidAmount).WithMessage("Please increase the price."); ;

            RuleFor(v => v.ReservePrice)
                .GreaterThan(v => v.StartingBidAmount).WithMessage("Please increase the price."); ;

            RuleFor(v => v.DeliveryMethod)
                .Must(v => Enum.IsDefined(typeof(DeliveryMethod),v)).WithMessage("Invalid delivery method specified.")
                .NotEmpty().WithMessage("This field is required.");

            RuleFor(v => v.Status)
                .Must(v => Enum.IsDefined(typeof(Status), v)).WithMessage("Invalid status specified.");

            RuleFor(v => v.EndDate)
                .GreaterThanOrEqualTo(v => v.StartDate).WithMessage("Please choose a date in the future.");
        }
    }
}
