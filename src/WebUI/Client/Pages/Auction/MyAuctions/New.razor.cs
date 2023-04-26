using Blazored.Toast.Services;
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

    [Inject]
    public IToastService toastService { get; set; }

    public AuctionItemDetailsVM? Model { get; set; }

    public DeliveryMethod[] Methods = (DeliveryMethod[])Enum.GetValues(typeof(DeliveryMethod));

    protected async Task ShowWarnings(AuctionItemDTO item, string auctionType)
    {
        string message;

        if (auctionType == "add")
        {
            message = "The auction has been successfully added!";
            toastService.ShowSuccess(message);
        }
    }

    protected override async Task OnInitializedAsync()
    {
        Model = new AuctionItemDetailsVM(); 
        Model.Auction.StartDate = DateTime.Now;
        Model.Auction.EndDate = DateTime.Now;
    }

    public async Task AddAuction()
    {
        await AuctionsClient.AddAuctionAsync(Model.Auction);
        await ShowWarnings(Model.Auction, "add");

        Navigation.NavigateTo("/auctions");
    }
}
