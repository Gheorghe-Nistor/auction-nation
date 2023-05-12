using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.AccessControl;
using Cegeka.Auction.WebUI.Shared.Listings;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Cegeka.Auction.WebUI.Client.Pages.Auction.AllAuctions;

public partial class Index
{
    [Inject]
    public IListingsClient ListingsClient { get; set; } = null!;

    [Inject]
    public NavigationManager Navigation { get; set; } = null!;

    [Inject]
    private IUsersClient UsersClient { get; set; } = null!;

    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    [Inject]
    public IValuteClient Valute { get; set; } = null!;

    public UserDetailsVm UserDetails { get; set; }

    public decimal StartBidAccount { get; set; }
    public decimal CurentBidAccount { get; set; }

    public string? CurrentUserId { get; set; } = null;

    public ListingsVM? Model { get; set; }

    public Category[] Categories = (Category[])Enum.GetValues(typeof(Category));

    protected override async Task OnInitializedAsync()
    {
        Model = await ListingsClient.PostListingsAsync(new ListingsQueryParams { 
            Status = "InProgress",
            MinDate = DateTime.Now
        });

        var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
        var user = authState.User;

        if (user.Identity.IsAuthenticated)
        {
            CurrentUserId = await UsersClient.GetUserIdByUserNameAsync(user.Identity.Name);
            UserDetails = await UsersClient.GetUserAsync(CurrentUserId.ToString());
            if (UserDetails.User.CurrencyId != Model.Auctions && UserDetails.User.CurrencyId != null)
            {
                StartBidAccount = await Valute.ConvertCurrencyAsync(Enum.GetName(typeof(Currencies), Model.Auction.CurrencyId),
                    Enum.GetName(typeof(Currencies), UserDetails.User.CurrencyId), Model.Auction.StartingBidAmount);
                CurentBidAccount = await Valute.ConvertCurrencyAsync(Enum.GetName(typeof(Currencies), Model.Auction.CurrencyId),
                    Enum.GetName(typeof(Currencies), UserDetails.User.CurrencyId), Model.Auction.CurrentBidAmount);
                ReserveBidAccount = await Valute.ConvertCurrencyAsync(Enum.GetName(typeof(Currencies), Model.Auction.CurrencyId),
                    Enum.GetName(typeof(Currencies), UserDetails.User.CurrencyId), Model.Auction.ReservePrice);
                BuyItNowBidAccount = await Valute.ConvertCurrencyAsync(Enum.GetName(typeof(Currencies), Model.Auction.CurrencyId),
                    Enum.GetName(typeof(Currencies), UserDetails.User.CurrencyId), Model.Auction.BuyItNowPrice);
            }
        }
    }

    public async Task GetListings()
    {
        Model = await ListingsClient.PostListingsAsync(Model.QueryParams);

        StateHasChanged();
    }
}
