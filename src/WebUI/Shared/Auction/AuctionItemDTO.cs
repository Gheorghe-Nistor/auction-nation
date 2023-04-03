using Cegeka.Auction.WebUI.Shared.Bid;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.WebUI.Shared.Auction;

public class AuctionItemDTO
{
    public Guid PublicId { get; set; }

    [Required]
    [MaxLength(100, ErrorMessage = "Please make the title shorter")]
    public string Title { get; set; } = string.Empty;

    [Required]
    [MaxLength(1000)]
    public string Description { get; set; } = String.Empty;

    [Required]
    public decimal StartingBidAmount { get; set; } = 0;

    public decimal CurrentBidAmount { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime EndTime { get; set; } = DateTime.Now;

    [Required]
    [MaxLength(20, ErrorMessage = "Please shorten the shipping details")]
    public string ShippingDetails { get; set; } = string.Empty;

    public ICollection<BidDTO>? BiddingHistory { get; set; } = new List<BidDTO>();
    public string Status { get; set; } = "active";

    public AuctionItemDTO() { }

    public AuctionItemDTO(Guid publicId, string title = "", string description = "", decimal startingBidAmount = 0, decimal currentBidAmount = 0, DateTime? endTime = null, string shippingDetails = "", List<BidDTO>? biddingHistory = null, string status = "active")
    {
        PublicId = publicId;
        Title = title;
        Description = description;
        StartingBidAmount = startingBidAmount;
        CurrentBidAmount = currentBidAmount;
        EndTime = endTime ?? DateTime.Now.AddDays(30);
        ShippingDetails = shippingDetails;
        BiddingHistory = biddingHistory;
        Status = status;
    }

}
