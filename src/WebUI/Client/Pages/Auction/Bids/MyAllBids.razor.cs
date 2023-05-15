using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.AccessControl;
using Cegeka.Auction.WebUI.Shared.Auction;
using Cegeka.Auction.WebUI.Shared.Bid;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;

namespace Cegeka.Auction.WebUI.Client.Pages.Auction.Bids
{
    public partial class MyAllBids
    {
        [Inject]
        public IBidClient BidClient { get; set; }

        [Inject]
        private IUsersClient UsersClient { get; set; } = null!;
        [Inject]
        public IAuctionsClient AuctionsClient { get; set; } = null!;

        [Inject]
        private AuthenticationStateProvider AuthenticationStateProvider { get; set; } = null!;

        public BidVM? Bids { get; set; } = new BidVM();


        public string? CurrentUserId { get; set; } = null;

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                CurrentUserId = await UsersClient.GetUserIdByUserNameAsync(user.Identity.Name);
            }

            Bids = await BidClient.GetBidByUserIdAsync(CurrentUserId);

        }

        public async Task<string> GetNameAsync(int auctionId)
        {
            AuctionItemDetailsVM _itemDTO = await AuctionsClient.GetAuctionAsync(auctionId.ToString());
            return _itemDTO.Auction.Title;
        }
    }
}
