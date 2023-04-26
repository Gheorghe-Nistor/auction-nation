using Blazored.Toast.Services;
using Cegeka.Auction.WebUI.Shared.Auction;
using Microsoft.AspNetCore.Components;

namespace Cegeka.Auction.WebUI.Client.Pages.Auction.MyAuctions;

public partial class Index
{
    [Inject]
    public IAuctionsClient AuctionsClient { get; set; } = null!;

    [Inject]
    public NavigationManager Navigation { get; set; } = null!;

    [Inject]
    public IToastService toastService { get; set; }

    public AuctionItemsVM? Model { get; set; }

    public ConfirmationDialog ConfirmationDeleteDialog { get; set; }

    private AuctionItemDTO _toDelete;

    public SortUtils Sorter { get; set; } = new SortUtils();

    protected override async Task OnInitializedAsync()
    {
        Model = await AuctionsClient.GetAuctionsAsync(); 
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
            Model.Auctions = Model.Auctions.OrderBy(property).ToList();
            currentSortDirection = "desc";
        }
        else
        {
            Model.Auctions = Model.Auctions.OrderByDescending(property).ToList();
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
                toastService.ShowWarning(message);
            }
            else if (auctionType == "delete")
            {
                message = "Deleting this auction may result in a penalty or fee!";
                toastService.ShowError(message);
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
