using Cegeka.Auction.Application.AuctionItems.Queries;
using Cegeka.Auction.Application.Listings.Queries;
using Cegeka.Auction.Application.TodoLists.Queries;
using Cegeka.Auction.Domain.Entities;
using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.Domain.ValueObjects;
using Cegeka.Auction.WebUI.Shared.Auction;
using Cegeka.Auction.WebUI.Shared.Listings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Application.SubcutaneousTests.Auctions.Queries;

using static Testing;
public class GetAuctionsTests : BaseTestFixture
{
    private async Task FeedData()
    {
        List<AuctionItem> auctions = new List<AuctionItem>()
        {
            new AuctionItem
            {
                PublicId = new Guid("68d0cbb6-09a6-4c05-a50a-c26d0c0e35b2"),
                Title = "Military Watch from WWII",
                Description = "This vintage military watch from the World War II era is a piece of history housed in a small 30-32mm case. Manufactured by renowned American watch companies Elgin, Waltham, and Bulova, it was made according to a standard set by the U.S. military. Don't miss out on the opportunity to own this unique piece of history!",
                Images = new List<string> {"N/A"},
                Category = Category.CollectiblesAndMemorabilia,
                StartingBidAmount = 500,
                StartDate = new DateTime(2023, 4, 1),
                EndDate = new DateTime(2023, 5, 1),
                BuyItNowPrice = 1000,
                ReservePrice = 800,
                DeliveryMethod = DeliveryMethod.DeliveryByCourier,
                Status = Status.New
            },
            new AuctionItem
            {
                PublicId = new Guid("8e6b68e6-9151-4d0b-a8ec-9473a4630c9f"),
                Title = "WWII Era Webley MK IV Revolver",
                Description = "This blued finish revolver is a six-shot fluted cylinder with fixed blade front and fixed rear notch sights. The frame is marked \"War Finish\" designating British acceptance during WWII. Don't miss the chance to own this historic piece of weaponry from one of the most tumultuous times in world history.",
                Images = new List<string> {"N/A"},
                Category = Category.CollectiblesAndMemorabilia,
                StartingBidAmount = 1000,
                StartDate = new DateTime(2023, 5, 1),
                EndDate = new DateTime(2023, 6, 1),
                BuyItNowPrice = 2000,
                ReservePrice = 1800,
                Status = Status.Approved,
                DeliveryMethod = DeliveryMethod.DeliveryByCourier
            },
            new AuctionItem
            {
                PublicId = new Guid("13134658-c68e-4ad2-b40b-9dfc95f76ae7"),
                Title = "Rare 1967 Ford Mustang Fastback",
                Description = "This rare 1967 Ford Mustang Fastback is a true beauty. With a sleek black exterior and a powerful V8 engine, this classic car is sure to turn heads wherever you go. The interior is in excellent condition and features classic Mustang styling. Don't miss the chance to own this piece of automotive history.",
                Images = new List<string> {"N/A"},
                Category = Category.Vehicles,
                StartingBidAmount = 50000,
                StartDate = new DateTime(2023, 5, 15),
                EndDate = new DateTime(2023, 6, 15),
                BuyItNowPrice = 700000,
                ReservePrice = 65000,
                Status = Status.Submitted,
                DeliveryMethod = DeliveryMethod.PersonalPickUp
            },
            new AuctionItem
            {
                PublicId = new Guid("26aa77e2-ebd2-417c-9cb8-7cfe695548ab"),
                Title = "Antique Persian Rug",
                Description = "This beautiful antique Persian rug is a hand-knotted wool masterpiece. It features intricate floral and geometric designs in rich, warm colors. The craftsmanship and attention to detail are evident in every knot. Don't miss the opportunity to own this stunning piece of art.",
                Images = new List<string> {"N/A"},
                Category = Category.HomeAndGardening,
                StartingBidAmount = 2000,
                StartDate = new DateTime(2023, 4, 1),
                EndDate = new DateTime(2023, 6, 1),
                BuyItNowPrice = 4000,
                ReservePrice = 3000,
                Status = Status.New,
                DeliveryMethod = DeliveryMethod.PersonalPickUp
            },

            new AuctionItem
            {
                PublicId = new Guid("b6fda651-b1c9-44b8-b7b2-3423d6e83c6f"),
                Title = "Vintage Gibson Les Paul Electric Guitar",
                Description = "This vintage Gibson Les Paul electric guitar is a true classic. Made in the USA in the 1970s, it has a beautiful cherry sunburst finish and features dual humbucking pickups. It has been well-maintained and is in excellent playing condition. Don't miss the chance to own this iconic instrument.",
                Images = new List<string> {"N/A"},
                Category = Category.Music,
                StartingBidAmount = 5000,
                StartDate = new DateTime(2023, 5, 25),
                EndDate = new DateTime(2023, 6, 25),
                BuyItNowPrice = 10000,
                ReservePrice = 8000,
                Status = Status.Submitted,
                DeliveryMethod = DeliveryMethod.PersonalPickUp
            }
        };

        foreach (AuctionItem auctionItem in auctions)
        {
            await AddAsync(auctionItem);
        }
    }

    [Test]
    public async Task ShouldReturnNothingWhenThereAreNoAuctionsThatMatchtheCriteria()
    {
        // arrange
        await RunAsDefaultUserAsync();
        await FeedData();

        // act 
        var query = new GetListingsQuery(new ListingsQueryParams { Search = "ABC123" });
        var result = await SendAsync(query);

        // assert
        result.Auctions.Should().HaveCount(0);
    }

    [Test]
    public async Task ShouldReturnAllAuctions()
    {
        // arrange
        await RunAsDefaultUserAsync();
        await FeedData();

        // act 
        var query = new GetListingsQuery();
        var result = await SendAsync(query);

        // assert
        result.Auctions.Should().HaveCount(5);
    }

    [Test]
    public async Task ShouldReturnOnlyAuctionsThatMatchAStringInTheirTitle()
    {
        // arrange
        await RunAsDefaultUserAsync();
        await FeedData();

        // act 
        var query = new GetListingsQuery(new ListingsQueryParams{ Search = "WWII"});
        var result = await SendAsync(query);

        // assert
        result.Auctions.Should().HaveCount(2);
        result.Auctions[0].PublicId.Should().Be("68d0cbb6-09a6-4c05-a50a-c26d0c0e35b2");
        result.Auctions[1].PublicId.Should().Be("8e6b68e6-9151-4d0b-a8ec-9473a4630c9f");
    }

    [Test]
    public async Task ShouldReturnOnlyAuctionsThatMatchAStringInTheirDescription()
    {
        // arrange
        await RunAsDefaultUserAsync();
        await FeedData();

        // act 
        var query = new GetListingsQuery(new ListingsQueryParams { Search = "craftsmanship" });
        var result = await SendAsync(query);

        // assert
        result.Auctions.Should().HaveCount(1);
        result.Auctions[0].PublicId.Should().Be("26aa77e2-ebd2-417c-9cb8-7cfe695548ab");
    }


    /*
    [Test]
    public async Task ShouldReturnOnlyAuctionsThatHaveASpecifiedStatus()
    {
        // arrange
        await RunAsDefaultUserAsync();
        await FeedData();

        // act 
        var query = new GetListingsQuery(new ListingsQueryParams { Status = "New" });
        var result = await SendAsync(query);

        // assert
        result.Auctions.Should().HaveCount(2);
        result.Auctions[0].PublicId.Should().Be("68d0cbb6-09a6-4c05-a50a-c26d0c0e35b2");
        result.Auctions[1].PublicId.Should().Be("26aa77e2-ebd2-417c-9cb8-7cfe695548ab");
    }*/

    [Test]
    public async Task ShouldReturnOnlyAuctionsThatHaveASpecifiedDeliveryMethod()
    {
        // arrange
        await RunAsDefaultUserAsync();
        await FeedData();

        // act 
        var query = new GetListingsQuery(new ListingsQueryParams { DeliveryMethod = "PersonalPickUp" });
        var result = await SendAsync(query);

        // assert
        result.Auctions.Should().HaveCount(3);
        result.Auctions[0].PublicId.Should().Be("13134658-c68e-4ad2-b40b-9dfc95f76ae7");
        result.Auctions[1].PublicId.Should().Be("26aa77e2-ebd2-417c-9cb8-7cfe695548ab");
        result.Auctions[2].PublicId.Should().Be("b6fda651-b1c9-44b8-b7b2-3423d6e83c6f");
    }

    /*[Test]
    public async Task ShouldReturnOnlyAuctionsThatHaveASpecifiedCategory()
    {
        // arrange
        await RunAsDefaultUserAsync();
        await FeedData();

        // act 
        var query = new GetListingsQuery(new ListingsQueryParams { Category = "Vehicle" });
        var result = await SendAsync(query);

        // assert
        result.Auctions.Should().HaveCount(1);
        result.Auctions[0].PublicId.Should().Be("13134658-c68e-4ad2-b40b-9dfc95f76ae7");
    }
    */

    [Test]
    public async Task ShouldReturnOnlyTheAuctionsThatFallWithinTheSpecifiedPriceRange()
    {
        // arrange
        await RunAsDefaultUserAsync();
        await FeedData();

        // act 
        var query = new GetListingsQuery(new ListingsQueryParams { MinPrice = 1000, MaxPrice = 5000 });
        var result = await SendAsync(query);

        // assert
        result.Auctions.Should().HaveCount(3);
        result.Auctions[0].PublicId.Should().Be("8e6b68e6-9151-4d0b-a8ec-9473a4630c9f");
        result.Auctions[1].PublicId.Should().Be("26aa77e2-ebd2-417c-9cb8-7cfe695548ab");
        result.Auctions[2].PublicId.Should().Be("b6fda651-b1c9-44b8-b7b2-3423d6e83c6f");
    }

    [Test]
    public async Task ShouldReturnOnlyTheAuctionsThatOccurWithinTheSpecifiedDateRange()
    {
        // arrange
        await RunAsDefaultUserAsync();
        await FeedData();

        // act 
        var query = new GetListingsQuery(new ListingsQueryParams { MinDate = new DateTime(2023, 5, 20), MaxDate = new DateTime(2023, 6, 1) });
        var result = await SendAsync(query);

        // assert
        result.Auctions.Should().HaveCount(4);
        result.Auctions[0].PublicId.Should().Be("8e6b68e6-9151-4d0b-a8ec-9473a4630c9f");
        result.Auctions[1].PublicId.Should().Be("13134658-c68e-4ad2-b40b-9dfc95f76ae7");
        result.Auctions[2].PublicId.Should().Be("26aa77e2-ebd2-417c-9cb8-7cfe695548ab");
        result.Auctions[3].PublicId.Should().Be("b6fda651-b1c9-44b8-b7b2-3423d6e83c6f");
    }
    
    /*[Test]
    public async Task ShouldReturnOnlyTheAuctionThatMatchMultipleCriteria()
    {
        // arrange
        await RunAsDefaultUserAsync();
        await FeedData();

        // act 
        var query = new GetListingsQuery(new ListingsQueryParams
        { 
            Search = "Ford",
            Category = "Vehicle",
            Status = "Submitted",
            DeliveryMethod = "PersonalPickUp",
            MinPrice = 5000,
            MaxPrice = 50005,
            MinDate = new DateTime(2023, 5, 10),
            MaxDate = new DateTime(2023, 7, 10)
        });

        var result = await SendAsync(query);

        // assert
        result.Auctions.Should().HaveCount(1);
        result.Auctions.First().PublicId.Should().Be("13134658-c68e-4ad2-b40b-9dfc95f76ae7");
    }
    */
}
