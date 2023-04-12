using Cegeka.Auction.WebUI.Shared.Auction;
using Microsoft.AspNetCore.Components;

namespace Cegeka.Auction.WebUI.Client.Pages.Auction.MyAuctions;

public partial class Index
{
    [Inject]
    public IAuctionsClient AuctionsClient { get; set; } = null!;

    [Inject]
    public NavigationManager Navigation { get; set; } = null!;

    public AuctionItemsVM? Model { get; set; }

    public ConfirmationDialog ConfirmationDeleteDialog { get; set; }

    private AuctionItemDTO _toDelete;

    protected override async Task OnInitializedAsync()
    {
        Model = await AuctionsClient.GetAuctionsAsync();
    }

    protected async Task DeleteItem(AuctionItemDTO item)
    {
        _toDelete = item;
        ConfirmationDeleteDialog.Show();
    }

    protected async void OnConfirmationDeleteDialogClosed(bool arg)
    {
        if (arg == false)
        {
            return;
        }
        
        await AuctionsClient.DeleteAuctionItemAsync(_toDelete.Id);
        this.StateHasChanged();
        Navigation.NavigateTo("/auctions", forceLoad: true);
    }
}
