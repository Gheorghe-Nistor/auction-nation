using Blazored.Toast.Services;
using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.Auction;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Newtonsoft.Json.Linq;
using System;

namespace Cegeka.Auction.WebUI.Client.Pages.Auction.MyAuctions;

public partial class Index
{
    [Inject]
    public IAuctionsClient AuctionsClient { get; set; } = null!;

    [Inject]
    private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

    [Inject]
    private IUsersClient UsersClient { get; set; } = null!;

    [Inject]
    public NavigationManager Navigation { get; set; } = null!;

    [Inject]
    public IToastService ToastService { get; set; }

    public AuctionItemsVM? CreatedAuctions { get; set; }
    public AuctionItemsVM? WonAuctions { get; set; }

    public string CurrentUserId { get; set; }

    public List<AuctionItemDTO>? FilteredAuctions { get; set; }

    public ConfirmationDialog ConfirmationDeleteDialog { get; set; }

    private AuctionItemDTO _toDelete;

    public SortUtils Sorter { get; set; } = new SortUtils();

    public FilterUtils Filter { get; set; } = new FilterUtils();

    public Category[] Categories = (Category[])Enum.GetValues(typeof(Category));

    public PublicStatus[] Statuses = (PublicStatus[])Enum.GetValues(typeof(PublicStatus));

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

        FilteredAuctions = null;
    }

    private void FilterAuctions()
    {
        if (Filter.IsEmpty())
        {
            FilteredAuctions = null;
            return;
        }

        FilteredAuctions = CreatedAuctions.Auctions;

        if (Filter.Category != Category.None)
        {
            FilteredAuctions = FilteredAuctions
                                .Where(a => a.Category == Filter.Category)
                                .ToList();
        }
        
        if (Filter.Status != PublicStatus.None)
        {
            if (Filter.Status == PublicStatus.Active)
            {
                FilteredAuctions = FilteredAuctions
                                    .Where(a => a.EndDate > DateTime.Now)
                                    .ToList();
            }
            else
            {
                FilteredAuctions = FilteredAuctions
                                    .Where(a => a.EndDate <= DateTime.Now)
                                    .ToList();
            }
        }
    }

    private void SortAuctions(string columnName)
    {
        string currentSortDirection;
        Func<AuctionItemDTO, string> property;

        switch(columnName)
        {
            case "Title":
                currentSortDirection = Sorter.ByTitle;
                property = a => a.Title;
                break;

            case "Category":
                currentSortDirection = Sorter.ByCategory;
                property = a => a.Category.ToString();
                break;

            case "Start Date":
                currentSortDirection = Sorter.ByStartDate;
                property = a => a.StartDate.ToString();
                break;

            case "End Date":
                currentSortDirection = Sorter.ByEndDate;
                property = a => a.EndDate.ToString();
                break;

            default:
                throw new ArgumentException($"Invalid column name: {columnName}");
        }

        if (currentSortDirection == "asc")
        {
            CreatedAuctions.Auctions = CreatedAuctions.Auctions.OrderBy(property).ToList();
            currentSortDirection = "desc";
        }
        else
        {
            CreatedAuctions.Auctions = CreatedAuctions.Auctions.OrderByDescending(property).ToList();
            currentSortDirection = "asc";
        }

        switch (columnName)
        {
            case "Title":
                Sorter.ByTitle = currentSortDirection;
                break;

            case "Category":
                Sorter.ByCategory = currentSortDirection;
                break;

            case "Start Date":
                Sorter.ByStartDate = currentSortDirection;
                break;

            case "End Date":
                Sorter.ByEndDate = currentSortDirection;
                break;

            default:
                throw new ArgumentException($"Invalid column name: {columnName}");
        }
    }

    protected async Task ShowWarnings(AuctionItemDTO item, string auctionType)
    {
        TimeSpan diff = item.EndDate - DateTime.Now;
        string message;

        if (diff.TotalHours < 24)
        {
            if (auctionType == "edit")
            {
                message = "Editing this auction may result in a penalty or fee!";
                ToastService.ShowWarning(message);
            }
            else if (auctionType == "delete")
            {
                message = "Deleting this auction may result in a penalty or fee!";
                ToastService.ShowError(message);
            }
        }
    }

    protected async Task DeleteItem(AuctionItemDTO item)
    {
        _toDelete = item;
        await ShowWarnings(item, "delete");
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
