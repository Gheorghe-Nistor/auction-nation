using Cegeka.Auction.Domain.Common;
using Cegeka.Auction.Domain.CompareAttributes;
using Cegeka.Auction.Domain.Enums;
using Microsoft.AspNetCore.Components.Forms;
using System.ComponentModel.DataAnnotations;

namespace Cegeka.Auction.Domain.Entities;

public class AuctionItem : BaseAuditableEntity
{
    public int Id { get; set; }
    public Guid PublicId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; }

    [Required]
    [MaxLength(500)]
    public string Description { get; set; }

    [Required]
    public List<string> Images { get; set; } = new List<string>();

    [Required]
    public DateTime StartDate { get; set; }

    [Required]
    [GreaterThanOrEqualToDate(nameof(StartDate))]
    public DateTime EndDate { get; set; }

    [Required]
    public Category Category { get; set; }

    [Required]
    public decimal StartingBidAmount { get; set; }

    [Required]
    public int CurrencyId { get; set; }

    public decimal CurrentBidAmount { get; set; } = 0;

    [Required]
    [GreaterThanDecimal(nameof(StartingBidAmount))]
    public decimal BuyItNowPrice { get; set; }

    [Required]
    [GreaterThanDecimal(nameof(StartingBidAmount))]
    public decimal ReservePrice { get; set; }

    [Required]
    public DeliveryMethod DeliveryMethod { get; set; }

    [Required]
    public Status Status { get; set; }

    public List<Bid> BiddingHistory { get; set; } = new List<Bid>();
    public float Lat { get; set; }
    public float Lon { get; set; }

    public string? WinningBidder { get; set; } = null;
}
