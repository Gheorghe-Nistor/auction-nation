using Cegeka.Auction.WebUI.Shared.AccessControl;
using Cegeka.Auction.WebUI.Shared.Auction;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Cegeka.Auction.WebUI.Client.Pages.Auction.MyAuctions;

public partial class Index
{
    [Inject]
    public IAuctionsClient AuctionsClient { get; set; } = null!;

    public AuctionItemsVM? Model { get; set; }

    protected override async Task OnInitializedAsync()
    {
        Model = await AuctionsClient.GetAuctionsAsync();
    }
}
