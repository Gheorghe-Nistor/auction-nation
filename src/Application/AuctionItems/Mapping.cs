using Cegeka.Auction.WebUI.Shared.Auction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Application.AuctionItems;

public class Mapping
{
    public static AuctionItemDTO DTOFromEntity(AuctionItem entity)
    {
        return new AuctionItemDTO
        {
            Id = entity.PublicId,
            Title = entity.Title,
            Description = entity.Description,
            Images = entity.Images,
            Category = entity.Category,
            StartingBidAmount = entity.StartingBidAmount,
            CurrentBidAmount = entity.CurrentBidAmount,
            BuyItNowPrice = entity.BuyItNowPrice,
            ReservePrice = entity.ReservePrice,
            StartDate= entity.StartDate,
            EndDate = entity.EndDate,
            DeliveryMethod = entity.DeliveryMethod,
            Status = entity.Status,
            //BiddingHistory = entity.BiddingHistory,           // TODO: make conversion viable
        };
    }
}
