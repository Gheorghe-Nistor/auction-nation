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

        private string _bid { get; set; }

        public string message { get; set; } = "The bid must be a number.";
        private bool _isBid { get; set; } = false;

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
                _bid = "";
            }
        }

        private void ValidateDecimal(ChangeEventArgs args)
        {
            if (decimal.TryParse(args.Value.ToString(), out decimal result))
            {
                _bid = result.ToString();
                Amount = result;
                _isBid = false;
            }
            else
            {
                Amount = 0;
                _isBid = true;
                _bid = "";
            }
        }
    }
}
