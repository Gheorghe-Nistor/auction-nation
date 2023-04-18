using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.Auction;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System.Net.Http.Headers;
using System.Text;

namespace Cegeka.Auction.WebUI.Client.Pages.Auction.MyAuctions;

public partial class Edit
{
    [Parameter]
    public string AuctionId { get; set; } = null!;

    [Inject]
    public IAuctionsClient AuctionsClient { get; set; } = null!;

    [Inject]
    public NavigationManager Navigation { get; set; } = null!;

    public AuctionItemDetailsVM? Model { get; set; }

    public DeliveryMethod[] Methods = (DeliveryMethod[])Enum.GetValues(typeof(DeliveryMethod));

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

    public async Task UpdateAuction()
    {
        await AuctionsClient.PutAuctionItemAsync(Model.Auction.Id, Model.Auction);

        Navigation.NavigateTo("/auctions");
    }
}
