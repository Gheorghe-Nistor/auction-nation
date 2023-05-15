using Cegeka.Auction.WebUI.Shared.Bid;
using Cegeka.Auction.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using Cegeka.Auction.Domain.CompareAttributes;
using Microsoft.AspNetCore.Components.Forms;
using Cegeka.Auction.Domain.Common;

namespace Cegeka.Auction.WebUI.Shared.Auction;

public class AuctionItemDTO
{
    public int Id { get; set; }
    public Guid PublicId { get; set; }

    public string? CreatedBy { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [MaxLength(100, ErrorMessage = "Please make the title shorter.")]
    public string Title { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [MaxLength(500, ErrorMessage = "Please make the description shorter.")]
    public string Description { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    public List<string> Images { get; set; } = new List<string>();

    [Required(ErrorMessage = "This field is required.")]
    public DateTime StartDate { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [GreaterThanOrEqualToDate(nameof(StartDate), ErrorMessage = "Please choose a date in the future.")]
    public DateTime EndDate { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [Range(1, double.PositiveInfinity, ErrorMessage = "Please pick a valid category.")]
    [EnumDataType(typeof(Category))]
    public Category Category { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    public decimal StartingBidAmount { get; set; }

    public int CurrencyId { get; set; }

    public decimal CurrentBidAmount { get; set; } = 0;

    [Required(ErrorMessage = "This field is required.")]
    [GreaterThanDecimal(nameof(StartingBidAmount), ErrorMessage = "Please increase the price.")]
    public decimal BuyItNowPrice { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [GreaterThanDecimal(nameof(StartingBidAmount), ErrorMessage = "Please increase the price.")]
    public decimal ReservePrice { get; set; }

    [Required(ErrorMessage = "This field is required.")]
    [Range(1, double.PositiveInfinity, ErrorMessage = "Please pick a valid delivery method.")]
    [EnumDataType(typeof(DeliveryMethod))]
    public DeliveryMethod DeliveryMethod { get; set; }

    public int Status { get; set; }

    public PublicStatus PublicStatus { get; set; } = PublicStatus.None;

    public List<BidDTO> BiddingHistory { get; set; } = new List<BidDTO>();

    [Range(-85,85, ErrorMessage = "Latitude can be: -85 to +85")]
    public float Lat { get; set; }
    [Range(-180, 180, ErrorMessage = "Longitude can be: -180 to +180")]
    public float Lon { get; set; }

    public string? WinningBidder { get; set; }

    public AuctionItemDTO()
    {
    }

   
    public AuctionItemDTO(int id, Guid publicId, int Currency,  string title = "", string description ="", List<string> images = null, 
        Category category = Category.None, decimal startingBidAmount = 0, decimal currentBidAmount = 0, decimal buyItNowPrice = 0, 
        decimal reservePrice = 0, DateTime? startDate = null, DateTime? endDate = null, DeliveryMethod deliveryMethod = default, 
        List<BidDTO> biddingHistory = null, Status status = default, float lat=0, float lon=0, string? winningBidder = "")
    {
        Id = id;
        PublicId = publicId;
        Title = title;
        Description = description;
        Images = images;
        Category = category;
        StartingBidAmount = startingBidAmount;
        CurrencyId = Currency;
        CurrentBidAmount = currentBidAmount;
        BuyItNowPrice = buyItNowPrice;
        ReservePrice = reservePrice;
        StartDate = (DateTime) startDate;
        EndDate = endDate ?? DateTime.Now;
        DeliveryMethod = deliveryMethod;
        BiddingHistory = biddingHistory ?? new List<BidDTO>();
        Status = (int) status;
        Lat = lat;
        Lon = lon;
        WinningBidder = winningBidder;
    }
}
