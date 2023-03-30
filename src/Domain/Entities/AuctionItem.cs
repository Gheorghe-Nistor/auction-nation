using Cegeka.Auction.Domain.Common;
using Cegeka.Auction.Domain.Enums;
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
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public List<string> Images { get; set; } = new List<string>();
    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; }
    public string Category { get; set; } = string.Empty;

    public decimal StartingBidAmount { get; set; } = 0;

    public decimal? CurrentBidAmount { get; set; }

    public decimal? BuyItNowPrice { get; set; }

    public decimal? ReservePrice { get; set; }

    public DeliveryMethod DeliveryMethod { get; set; }

    public Status Status { get; set; } 

    public List<Bid> BiddingHistory { get; set; } = new List<Bid>();


}
