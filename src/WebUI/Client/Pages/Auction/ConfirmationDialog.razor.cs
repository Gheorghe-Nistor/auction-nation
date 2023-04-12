using Microsoft.AspNetCore.Components;

namespace Cegeka.Auction.WebUI.Client.Pages.Auction;

public partial class ConfirmationDialog
{
    [Parameter]
    public string Title { get; set; }

    [Parameter]
    public string Content { get; set; }

    [Parameter]
    public EventCallback<bool> CloseEventCallback { get; set; }

    protected bool ShowModal { get; set; } = false;

    public void Close()
    {
        ShowModal = false;
    }

    public void Show()
    {
        ShowModal = true;
        this.StateHasChanged();
    }

    public void OnCancelClicked()
    {
        Close();
    }

    public async void OnOkClicked()
    {
        await CloseEventCallback.InvokeAsync(true);
        Close();
    }
}
