using Cegeka.Auction.Domain.Entities;
using Microsoft.AspNetCore.Components;

namespace Cegeka.Auction.WebUI.Client.Pages.Auction.Bids
{
    public partial class BidDialog
    {
        [Parameter]
        public string Title { get; set; }
        [Parameter]
        public string Content { get; set; }

        [Parameter]
        public EventCallback<bool> PlaceBidEventCallback { get; set; }

        public string Bid { get; set; }

        public bool isBid = false;

        public decimal Amount { get; set; } = 0;

        protected bool ShowModal { get; set; }

        public void Close()
        {
            ShowModal = false;
        }

        public void Show()
        {
            ShowModal = true;
        }

        public void OnCancelClicked()
        {
            Close();
        }

        public async void OnOkClicked()
        {
            if (Amount != 0)
            {
                await PlaceBidEventCallback.InvokeAsync(true);
                Close();
                Bid = "";
                isBid = false;
            }
        }

        private void ValidateDecimal(ChangeEventArgs args)
        {
            if (decimal.TryParse(args.Value.ToString(), out decimal result))
            {
                Bid = result.ToString();
                Amount = result;
                isBid = false;
            }
            else
            {
                Amount = 0;
                isBid = true;
                Bid = "";
            }
        }
    }
}
