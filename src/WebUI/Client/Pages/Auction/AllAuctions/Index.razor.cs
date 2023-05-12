using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.Listings;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Reflection;

namespace Cegeka.Auction.WebUI.Client.Pages.Auction.AllAuctions;

public partial class Index
{
    [Inject]
    public IListingsClient ListingsClient { get; set; } = null!;

    [Inject]
    public NavigationManager Navigation { get; set; } = null!;

    public ListingsVM? Model { get; set; }

    public Category[] Categories = (Category[]) Enum.GetValues(typeof(Category));

    public PublicStatus[] Statuses = (PublicStatus[])Enum.GetValues(typeof(PublicStatus));

    protected override async Task OnInitializedAsync()
    {
        Model = await ListingsClient.PostListingsAsync(new ListingsQueryParams());
    }

    public async Task GetListings()
    {
        Model = await ListingsClient.PostListingsAsync(Model.QueryParams);

        StateHasChanged();
    }
}
