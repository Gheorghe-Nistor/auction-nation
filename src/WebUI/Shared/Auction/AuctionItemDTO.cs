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

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public List<string> Images { get; set; } = new List<string>();

    public string Category { get; set; } = string.Empty;

    public decimal StartingBidAmount { get; set; } = 0;

    public decimal CurrentBidAmount { get; set; }

    public decimal BuyItNowPrice { get; set; }

    public decimal ReservePrice { get; set; }

    public DateTime StartDate { get; set; } = DateTime.Now;
    public DateTime EndDate { get; set; } 

    public string DeliveryMethod { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;

    public List<BidDTO> BiddingHistory { get; set; } = new List<BidDTO>();

    public AuctionItemDTO()
    {

    }

    public AuctionItemDTO(Guid id, string title = "", string description ="", List<string> images = null, string category = "", decimal startingBidAmount = 0, decimal currentBidAmount = 0, decimal buyItNowPrice = 0, decimal reservePrice = 0, DateTime? startDate = null, DateTime? endDate = null, string deliveryMethod = "", List<BidDTO> biddingHistory = null, string status = "")
    {
        Id = id;
        Title = title;
        Description = description;
        Images = images;
        Category = category;
        StartingBidAmount = startingBidAmount;
        CurrentBidAmount = currentBidAmount;
        BuyItNowPrice = buyItNowPrice;
        ReservePrice = reservePrice;
        EndDate = endDate ?? DateTime.Now;
        DeliveryMethod = deliveryMethod;
        Status = status;
        BiddingHistory = biddingHistory;
    }

}
