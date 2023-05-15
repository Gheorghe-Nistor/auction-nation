using Blazored.Toast.Services;
using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Client.Pages.Auction.Bids;
using Cegeka.Auction.WebUI.Shared.AccessControl;
using Cegeka.Auction.WebUI.Shared.Auction;
using Cegeka.Auction.WebUI.Shared.Bid;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Cegeka.Auction.WebUI.Client.Pages.Auction.MyAuctions
{
    public partial class Read
    {
        private TimeSpan TimeLeft { get; set; }

        [Inject]
        public IToastService ToastService { get; set; }

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


        private string selectedSlide;

        public AuctionItemDetailsVM? Model { get; set; }

        public List<BidVM> BiddingHistory { get; set; } = new List<BidVM>();

        public DeliveryMethod[] Methods = (DeliveryMethod[])Enum.GetValues(typeof(DeliveryMethod));

        public Status[] StatusList = (Status[])Enum.GetValues(typeof(Status));

        public string? CurrentUserId { get; set; } = null;
        public string? AuctioneerUsername { get; set; } = null;
        public string? WinningBidderUsername { get; set; } = null;

        public bool BuyItNowAvailable { get; set; } = false;
        public bool BidAvailable { get; set; } = false;

        private BidDialog _bidDialog { get; set; }

        protected async Task PlaceBidForItem()
        {
            _bidDialog.Show();
        }

        protected async Task DialogAddForPlaceBid(bool arg)
        {
            if (!arg)
            {
                return;
            }

            decimal? maxBidAmount;

            if (Model.Auction.BiddingHistory == null || Model.Auction.BiddingHistory.Count() == 0)
            {
                maxBidAmount = Model.Auction.StartingBidAmount;
            }
            else
            {
                maxBidAmount = Model.Auction.BiddingHistory.Last().Amount;
            }

            if (_bidDialog.Amount + 1 >= maxBidAmount && _bidDialog.Amount > Model.Auction.StartingBidAmount)
            {
                var bid = new BidDTO
                {
                    Amount = _bidDialog.Amount,
                    CurrencyId = Model.Auction.CurrencyId
                };

                await AuctionsClient.PlaceAuctionBidAsync(Model.Auction.Id, bid);
                Model = await AuctionsClient.GetAuctionAsync(auctionId);

                ToastService.ShowSuccess("Bid placed successfully!");

                GetBiddingHistoryVM();

                StateHasChanged();
            }

            else
            {
                ToastService.ShowError("Your bid amount is less than the previous bid placed.");
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

            if (DateTime.Now < Model.Auction.EndDate && CurrentUserId != Model.Auction.CreatedBy && Model.Auction.WinningBidder == null)
            {
                if (DateTime.Now < Model.Auction.StartDate)
                {
                    BuyItNowAvailable = true;
                }
                else if (Model.Auction.Status == (int)Status.InProgress)
                {
                    BidAvailable = true;
                }
            }

            WinningBidderUsername = await GetUsernameFromUserId(Model.Auction.WinningBidder);

            AuctioneerUsername = await GetUsernameFromUserId(Model.Auction.CreatedBy);

            GetBiddingHistoryVM();

            var endDate = Model.Auction.EndDate;
            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += (sender, e) =>
            {
                var timeLeft = endDate - DateTime.Now;
                if (timeLeft < TimeSpan.Zero)
                {
                    timeLeft = TimeSpan.Zero;
                }
                TimeLeft = timeLeft;
                StateHasChanged();
            };
            timer.Start();

            selectedSlide = "0";
        }

        public async Task BuyAuction()
        {
            ToastService.ShowSuccess("Item purchased successfully!");

            await AuctionsClient.BuyAuctionItemAsync(Model.Auction.Id, CurrentUserId);

            Model = await AuctionsClient.GetAuctionAsync(auctionId);

            WinningBidderUsername = await GetUsernameFromUserId(Model.Auction.WinningBidder);

            StateHasChanged();
        }

        public async Task ValidateAuction()
        {
            var maxBidAmount = Model.Auction.BiddingHistory.Last().Amount;

            if (maxBidAmount != null && Model.Auction.ReservePrice != null && maxBidAmount <= Model.Auction.ReservePrice)
            {
                ToastService.ShowError("Unable to accept last bid offer. Amount below reserve price.");

                return;
            }

            ToastService.ShowSuccess("Last bid accepted!");

            await AuctionsClient.ValidateAuctionItemAsync(CurrentUserId, Model.Auction.Id);

            Model = await AuctionsClient.GetAuctionAsync(auctionId);

            WinningBidderUsername = await GetUsernameFromUserId(Model.Auction.WinningBidder);

            StateHasChanged();
        }

        private async Task GetBiddingHistoryVM()
        {
            BiddingHistory = new List<BidVM>();
            foreach (var bid in Model.Auction.BiddingHistory)
            {
                var bidderUser = await UsersClient.GetUserAsync(bid.CreatedBy);
                var bidderUsername = bidderUser.User.UserName;
                BiddingHistory.Add(new BidVM(bidderUsername, bid.Amount, bid.CreatedUtc, bid.CurrencyId));
            }
        }

        private async Task<string> GetUsernameFromUserId(string userId)
        {
            var user = await UsersClient.GetUserAsync(userId);
            return user.User.UserName;
        }
    }

}
