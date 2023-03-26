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
    public Guid Id { get; set; }

    [Required]
    [MaxLength(100, ErrorMessage = "Please make the title shorter")]
    public string Title { get; set; } = string.Empty;

    [Required]
    public int StartingBidAmount { get; set; } = 0;

    public int CurrentBidAmount { get; set; }

    [Required]
    [DataType(DataType.Date)]
    public DateTime EndTime { get; set; } = DateTime.Now;

    [Required]
    [MaxLength(20, ErrorMessage = "Please shorten the shipping details")]
    public string ShippingDetails { get; set; } = string.Empty;

    public ICollection<BidDTO> BiddingHistory { get; set; } = new List<BidDTO>();

    public AuctionItemDTO()
    {

    }

    public AuctionItemDTO(Guid id, string title = "", int startingBidAmount = 0, int currentBidAmount = 0, DateTime? endTime = null, string shippingDetails = "", List<BidDTO> biddingHistory = null)
    {
        Id = id;
        Title = title;
        StartingBidAmount = startingBidAmount;
        CurrentBidAmount = currentBidAmount;
        EndTime = endTime ?? DateTime.Now;
        ShippingDetails = shippingDetails;
        BiddingHistory = biddingHistory;
    }

}
