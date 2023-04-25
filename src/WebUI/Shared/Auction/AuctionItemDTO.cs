using Cegeka.Auction.WebUI.Shared.Bid;
using Cegeka.Auction.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Cegeka.Auction.Domain.CompareAttributes;
using Microsoft.AspNetCore.Components.Forms;

namespace Cegeka.Auction.WebUI.Shared.Auction;

public class AuctionItemDTO
{
    public int Id { get; set; }
    public Guid PublicId { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [MaxLength(100, ErrorMessage = "Please make the title shorter.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [MaxLength(500, ErrorMessage = "Please make the description shorter.")]
    public string Description { get; set; }

    public List<string> Images { get; set; } = new List<string>();

    [Required(ErrorMessage = "This field is required.")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [GreaterThanOrEqualToDate(nameof(StartDate), ErrorMessage = "Please choose a date in the future.")]
    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [MaxLength(50, ErrorMessage = "Please write a shorter category name.")]
    public string Category { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    public decimal StartingBidAmount { get; set; }

    public decimal CurrentBidAmount { get; set; } = 0;

    [Required]
    [GreaterThanDecimal(nameof(StartingBidAmount), ErrorMessage = "Please increasee the price.")]
    public decimal BuyItNowPrice { get; set; }

    [Required]
    [GreaterThanDecimal(nameof(StartingBidAmount), ErrorMessage = "Please increasee the price.")]
    public decimal ReservePrice { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [EnumDataType(typeof(DeliveryMethod))]
    public DeliveryMethod DeliveryMethod { get; set; }

    public int Status { get; set; }

    public List<BidDTO> BiddingHistory { get; set; } = new List<BidDTO>();

    public AuctionItemDTO()
    {
    }

    public AuctionItemDTO(int id, Guid publicId, string title = "", string description ="", List<string> images = null, string category = "", decimal startingBidAmount = 0, decimal currentBidAmount = 0, decimal buyItNowPrice = 0, decimal reservePrice = 0, DateTime? startDate = null, DateTime? endDate = null, DeliveryMethod deliveryMethod = default, List<BidDTO> biddingHistory = null, Status status = default)
    {
        Id = id;
        PublicId = publicId;
        Title = title;
        Description = description;
        Images = images;
        Category = category;
        StartingBidAmount = startingBidAmount;
        CurrentBidAmount = currentBidAmount;
        BuyItNowPrice = buyItNowPrice;
        ReservePrice = reservePrice;
        StartDate = (DateTime) startDate;
        EndDate = endDate ?? DateTime.Now;
        DeliveryMethod = deliveryMethod;
        BiddingHistory = biddingHistory;
        Status = (int) status;
    }
}
