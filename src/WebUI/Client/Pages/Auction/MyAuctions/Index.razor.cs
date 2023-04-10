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

    public AuctionItemDTO _toDelete;

    public ConfirmationDialog ConfirmationDeleteDialog { get; set; }

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
        if (arg)
        {
            await AuctionsClient.DeleteAuctionItemAsync(_toDelete.Id);
        }
    }
}
