using Cegeka.Auction.Domain.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Domain.Entities;

public class AuctionItem : BaseAuditableEntity
{
    public int Id { get; set; }

    public Guid PublicId { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = String.Empty;

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = String.Empty;

    [Required]
    public int StartingBidAmount { get; set; } = 0;

    public int CurrentBidAmount { get; set; }

    public DateTime EndTime { get; set; } = DateTime.Now.AddDays(30);

    [Required]
    [MaxLength(100)]
    public string ShippingDetails { get; set; } = String.Empty;

    public ICollection<Bid> BiddingHistory { get; set; } = new List<Bid>();

    public string Status { get; set; } = "active";
}
