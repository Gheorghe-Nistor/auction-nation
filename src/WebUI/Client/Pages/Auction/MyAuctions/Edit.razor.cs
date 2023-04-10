using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.Auction;
using Microsoft.AspNetCore.Components;

namespace Cegeka.Auction.WebUI.Client.Pages.Auction.MyAuctions;

public partial class Edit
{
    [Parameter]
    public string auctionId { get; set; } = null!;

    [Inject]
    public IAuctionsClient AuctionsClient { get; set; } = null!;

    [Inject]
    public NavigationManager Navigation { get; set; } = null!;

    public AuctionItemDetailsVM? Model { get; set; }

    public DeliveryMethod[] Methods = (DeliveryMethod[])Enum.GetValues(typeof(DeliveryMethod));

    protected override async Task OnParametersSetAsync()
    {
        Model = await AuctionsClient.GetAuctionAsync(auctionId);
    }

    public async Task UpdateAuction()
    {
        await AuctionsClient.PutAuctionItemAsync(Model.Auction.Id, Model.Auction);

        Navigation.NavigateTo("/auctions");
    }
}
