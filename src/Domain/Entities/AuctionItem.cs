using Cegeka.Auction.Domain.Common;
using Cegeka.Auction.Domain.CompareAttributes;
using Cegeka.Auction.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace Cegeka.Auction.Domain.Entities;

public class AuctionItem : BaseAuditableEntity
{
    public int Id { get; set; }
    //public Guid PublicId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MaxLength(500)]
    public string Description { get; set; }

    [Required]
    [MinLength(1)]
    public List<string> Images { get; set; } = new List<string>();

    public DateTime StartDate { get; set; } = DateTime.Now;

    [GreaterThanOrEqualToDate(nameof(StartDate))]
    public DateTime EndDate { get; set; } = DateTime.Now;

    [Required]
    [MaxLength(50)]
    public string Category { get; set; }

    [Required]
    public decimal StartingBidAmount { get; set; }

    public decimal? CurrentBidAmount { get; set; } = 0;

    [GreaterThanDecimal(nameof(StartingBidAmount))]
    public decimal? BuyItNowPrice { get; set; }

    [GreaterThanDecimal(nameof(StartingBidAmount))]
    public decimal? ReservePrice { get; set; }

    [Required]
    public DeliveryMethod DeliveryMethod { get; set; }

    public Status Status { get; set; }

    public List<Bid> BiddingHistory { get; set; } = new List<Bid>();
}
