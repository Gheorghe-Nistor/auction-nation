using Cegeka.Auction.WebUI.Shared.Auction;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Cegeka.Auction.WebUI.Client.Pages.Auction.MyAuctions;

public partial class Index
{
    [Inject]
    private IUsersClient UsersClient { get; set; } = null!;

    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    [Inject]
    public IAuctionsClient AuctionsClient { get; set; } = null!;

    [Inject]
    public NavigationManager Navigation { get; set; } = null!;

    public AuctionItemsVM? CreatedAuctions { get; set; }
    public AuctionItemsVM? WonAuctions { get; set; }

    public string? CurrentUserId { get; set; } = null;

    public ConfirmationDialog ConfirmationDeleteDialog { get; set; }

    private AuctionItemDTO _toDelete;

    protected override async Task OnInitializedAsync()
    {
        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity.IsAuthenticated)
        {
            CurrentUserId = await UsersClient.GetUserIdByUserNameAsync(user.Identity.Name);
        }

        CreatedAuctions = await AuctionsClient.GetCreatedAuctionsByUserIdAsync(CurrentUserId);

        WonAuctions = await AuctionsClient.GetWonAuctionsByUserIdAsync(CurrentUserId);
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
