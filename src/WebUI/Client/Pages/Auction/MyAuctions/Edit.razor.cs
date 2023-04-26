using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.Auction;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Web;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;

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
    public string ValidationMessage { get; set; } = string.Empty;


    protected override async Task OnParametersSetAsync()
    {
        Model = await AuctionsClient.GetAuctionAsync(AuctionId);
    }

    private async Task LoadFiles(InputFileChangeEventArgs e)
    {
        isLoading = true;
        ValidationMessage = string.Empty;

        foreach (var file in e.GetMultipleFiles(maxAllowedFiles))
        {
            // Validate file type
            if (!Regex.IsMatch(file.ContentType, @"^image\/(jpeg|png)$"))
            {
                // Invalid file type
                ValidationMessage = $"File {file.Name} is not of an allowed type.";
                return;
            }

            // Validate image file size
            if (file.Size > 5 * 1024 * 1024) // 5 MB
            {
                ValidationMessage = $"File {file.Name} exceeds the maximum size of 5 MB.";
                return;
            }

            try
            {
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
        if (loadedFiles.Any())
        {
            foreach (var file in loadedFiles)
            {
                try
                {
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
        }
        if(!Model.Auction.Images.Any())
        {
            ValidationMessage = "At least one image is required to update the auction.";
            return;
        }

        await AuctionsClient.PutAuctionItemAsync(Model.Auction.Id, Model.Auction);

        Navigation.NavigateTo("/auctions");
    }

    public async Task RemoveImage(int index)
    {
        if (index >= 0 && index < Model.Auction.Images.Count)
        {
            Model.Auction.Images.RemoveAt(index);
            StateHasChanged();
        }
    }

    private RenderFragment RenderImages()
    {
        return builder =>
        {
            for (int i = 0; i < Model.Auction.Images.Count; i++)
            {
                var index = i;
                builder.OpenElement(0, "div");
                builder.AddAttribute(1, "style", "display: inline-block; position: relative; margin-right: 10px;");

                builder.OpenElement(2, "img");
                builder.AddAttribute(3, "src", Model.Auction.Images[index]);
                builder.AddAttribute(4, "style", "max-height: 100px;");
                builder.AddAttribute(5, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, async e =>
                {
                    await RemoveImage(index);
                }));
                builder.CloseElement();

                builder.OpenElement(6, "span");
                builder.AddAttribute(7, "style", "position: absolute; top: -5px; right: -5px; display: inline-block; width: 20px; height: 20px; text-align: center; background-color: #f44336; color: white; cursor: pointer; border-radius: 50%;");
                builder.AddAttribute(8, "onclick", EventCallback.Factory.Create<MouseEventArgs>(this, async e =>
                {
                    await RemoveImage(index);
                }));
                builder.AddContent(9, "X");
                builder.CloseElement();

                builder.CloseElement();
            }
        };
    }
}
