using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.Auction;
using Microsoft.AspNetCore.Components;

namespace Cegeka.Auction.WebUI.Client.Pages.Auction.MyAuctions;

public partial class New
{
    [Inject]
    public IAuctionsClient AuctionsClient { get; set; } = null!;

    [Inject]
    public NavigationManager Navigation { get; set; } = null!;

    public AuctionItemDetailsVM? Model { get; set; }

    public DeliveryMethod[] Methods = (DeliveryMethod[])Enum.GetValues(typeof(DeliveryMethod));

    protected override async Task OnInitializedAsync()
    {
        Model = new AuctionItemDetailsVM(); 
        Model.Auction.StartDate = DateTime.Now;
        Model.Auction.EndDate = DateTime.Now;
    }

    public async Task AddAuction()
    {
        await AuctionsClient.AddAuctionAsync(Model.Auction);

        Navigation.NavigateTo("/auctions");
    }
}
