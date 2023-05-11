using Blazored.Toast.Services;
using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.Auction;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Cegeka.Auction.WebUI.Client.Pages.Auction.MyAuctions;

public partial class Edit
{
    [Parameter]
    public string AuctionId { get; set; } = null!;

    [Inject]
    public IAuctionsClient AuctionsClient { get; set; } = null!;

    [Inject]
    public NavigationManager Navigation { get; set; } = null!;

    [Inject]
    public IToastService? toastService { get; set; }

    public AuctionItemDetailsVM? Model { get; set; }

    public DeliveryMethod[] Methods = (DeliveryMethod[])Enum.GetValues(typeof(DeliveryMethod));

    public Category[] Categories = (Category[])Enum.GetValues(typeof(Category));

    public List<IBrowserFile> loadedFiles = new();

    public int maxAllowedFiles = 10;

    public bool isLoading;

    protected override async Task OnParametersSetAsync()
    {
        Model = await AuctionsClient.GetAuctionAsync(AuctionId);
    }

    private async Task LoadFiles(InputFileChangeEventArgs e)
    {
        isLoading = true;
        loadedFiles.Clear();
        Model.Auction.Images.Clear();

        foreach (var file in e.GetMultipleFiles(maxAllowedFiles))
        {
            try
            {
                // save IBrowserFile to display details
                loadedFiles.Add(file);

                // add image to auction item
                using var memoryStream = new MemoryStream();
                await file.OpenReadStream().CopyToAsync(memoryStream);
                var bytes = memoryStream.ToArray();
                var base64 = Convert.ToBase64String(bytes);
                var imgSrc = $"data:{file.ContentType};base64,{base64}";
                Model.Auction.Images.Add(imgSrc);
               
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        isLoading = false;
    }

    protected async Task ShowWarnings(AuctionItemDTO item, string auctionType)
    {
        string message;
        TimeSpan diff = item.EndDate - DateTime.Now;

        if (auctionType == "edit")
        {
            message = "The auction has been successfully updated!";
            toastService.ShowSuccess(message);
        }
    }

    public async Task UpdateAuction()
    {
        await AuctionsClient.PutAuctionItemAsync(Model.Auction.Id, Model.Auction);
        await ShowWarnings(Model.Auction, "edit");

        Navigation.NavigateTo("/auctions");
    }
}
