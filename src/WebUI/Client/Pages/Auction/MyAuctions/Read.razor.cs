using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Client.Pages.Auction.Bids;
using Cegeka.Auction.WebUI.Shared.Auction;
using Cegeka.Auction.WebUI.Shared.Bid;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Cegeka.Auction.WebUI.Client.Pages.Auction.MyAuctions
{
    public partial class Read
    {
        private TimeSpan TimeLeft { get; set; }

        [Parameter]
        public string auctionId { get; set; } = null!;

        [Inject]
        private IUsersClient UsersClient { get; set; } = null!;

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

        [Inject]
        public IAuctionsClient AuctionsClient { get; set; } = null!;

        [Inject]
        public IBidClient BidClient { get; set; }

        public AuctionItemDetailsVM? Model { get; set; }

        public DeliveryMethod[] Methods = (DeliveryMethod[])Enum.GetValues(typeof(DeliveryMethod));
        public string CurrentUserId { get; set; } = String.Empty;
        public bool BuyItNowAvailable { get; set; } = false;

        private BidDialog _bidDialog { get; set; }

        protected async Task PlaceBidForItem()
        {
            _bidDialog.Show();
        }

        protected async void DialogAddForPlaceBid(bool arg)
        {
            if (_bidDialog.Amount > Model.Auction.StartingBidAmount)
            {
                if (arg)
                {
                    BidDTO bid = new BidDTO()
                    {
                        Amount = _bidDialog.Amount,
                        ItemId = Model.Auction.Id
                    };
                    await BidClient.AddAuctionAsync(bid);

                    Model.Auction.CurrentBidAmount = bid.Amount;
                    Model.Auction.BiddingHistory.Add(bid);
                    await AuctionsClient.PutAuctionItemAsync(Model.Auction.Id, Model.Auction);
                    this.StateHasChanged();
                }
            }
        }

        protected override async Task OnInitializedAsync()
        {
            Model = await AuctionsClient.GetAuctionAsync(auctionId);

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                CurrentUserId = await UsersClient.GetUserIdByUserNameAsync(user.Identity.Name);
            }

            if (DateTime.Now < Model.Auction.StartDate && CurrentUserId != Model.Auction.CreatedBy)
            {
                BuyItNowAvailable = true;
            }

            var endDate = Model.Auction.EndDate;
            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += (sender, e) =>
            {
                TimeLeft = endDate - DateTime.Now;
                if (TimeLeft.TotalSeconds < 0)
                {
                    timer.Stop();
                }
                StateHasChanged();
            };
            timer.Start();
        }

    }

}
