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
    public string Title { get; set; } = string.Empty;

    [Required]
    public int StartingBidAmount { get; set; } = 0;

    public int CurrentBidAmount { get; set; }

    [Required]
    public DateTime EndTime { get; set; } = DateTime.Now;

    [Required]
    [MaxLength(20)]
    public string ShippingDetails { get; set; } = string.Empty;

    public ICollection<Bid> BiddingHistory { get; set; } = new List<Bid>();
}
