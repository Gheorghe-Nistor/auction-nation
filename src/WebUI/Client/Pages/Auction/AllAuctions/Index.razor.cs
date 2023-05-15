using Blazored.Toast.Services;
using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.AccessControl;
using Cegeka.Auction.WebUI.Shared.Auction;
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
    public IToastService ToastService { get; set; }

    public ListingsVM? Model { get; set; }

    public Category[] Categories = (Category[]) Enum.GetValues(typeof(Category));

    public PublicStatus[] Statuses = (PublicStatus[])Enum.GetValues(typeof(PublicStatus));


    protected override async Task OnInitializedAsync()
    {
        Model = await ListingsClient.PostListingsAsync(new ListingsQueryParams());
    }

    public async Task GetListings()
    {
        ToastService.ShowInfo("Searching auctions...");

        Model = await ListingsClient.PostListingsAsync(Model.QueryParams);

        StateHasChanged();
    }
}
