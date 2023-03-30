using Cegeka.Auction.WebUI.Shared.Auction;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;

namespace Cegeka.Auction.WebUI.Client.Pages.Auction;

public partial class Index
{
    public AuctionItemsVM Model { get; set; }

    [Inject]
    public HttpClient HttpClient { get; set; }

    protected override async Task OnInitializedAsync()
    {
        var array = await HttpClient.GetFromJsonAsync<AuctionItemDTO[]>("/auctions");
        Model = new AuctionItemsVM(array);
    }
}
