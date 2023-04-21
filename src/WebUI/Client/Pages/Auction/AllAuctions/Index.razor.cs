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

    protected override async Task OnInitializedAsync()
    {
        Model = await ListingsClient.PostListingsAsync(new ListingsQueryParams { 
            Status = "InProgress",
            MinDate = DateTime.Now
        });
    }

    public async Task GetListings()
    {
        Model = await ListingsClient.PostListingsAsync(Model.QueryParams);

        StateHasChanged();
    }
}
