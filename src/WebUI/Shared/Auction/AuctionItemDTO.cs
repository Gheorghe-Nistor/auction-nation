using Cegeka.Auction.WebUI.Shared.Bid;
using Cegeka.Auction.Domain.Enums;

namespace Cegeka.Auction.WebUI.Shared.Auction;

public class AuctionItemDTO
{
    public int Id { get; set; }

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

    public List<BidDTO> BiddingHistory { get; set; } = new List<BidDTO>();

    public AuctionItemDTO(int id, string title = "", string description ="", List<string> images = null, string category = "", decimal startingBidAmount = 0, decimal currentBidAmount = 0, decimal buyItNowPrice = 0, decimal reservePrice = 0, DateTime? startDate = null, DateTime? endDate = null, DeliveryMethod deliveryMethod = default, List<BidDTO> biddingHistory = null, Status status = default)
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
        BiddingHistory = biddingHistory;
        Status = status;
    }
}
