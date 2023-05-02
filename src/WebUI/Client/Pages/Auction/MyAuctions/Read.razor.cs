﻿using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.Auction;
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
        
        public AuctionItemDetailsVM? Model { get; set; }

        public DeliveryMethod[] Methods = (DeliveryMethod[])Enum.GetValues(typeof(DeliveryMethod));
        public string? CurrentUserId { get; set; } = null;
        public string? AuctioneerUsername { get; set; } = null;
        public string? WinningBidderUsername { get; set; } = null;
        public bool BuyItNowAvailable { get; set; } = false;

        protected override async Task OnInitializedAsync()
        {
            Model = await AuctionsClient.GetAuctionAsync(auctionId);

            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = authState.User;

            if (user.Identity.IsAuthenticated)
            {
                CurrentUserId = await UsersClient.GetUserIdByUserNameAsync(user.Identity.Name);
            }

            if (DateTime.Now < Model.Auction.StartDate && CurrentUserId != Model.Auction.CreatedBy && Model.Auction.WinningBidder == null)
            {
                BuyItNowAvailable = true;
            }

            var winningBidderUser = await UsersClient.GetUserAsync(Model.Auction.WinningBidder);
            WinningBidderUsername = winningBidderUser.User.UserName;

            var auctioneerUser = await UsersClient.GetUserAsync(Model.Auction.CreatedBy);
            AuctioneerUsername = auctioneerUser.User.UserName;

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

        public async Task BuyAuction()
        {
            await AuctionsClient.BuyAuctionItemAsync(Model.Auction.Id, CurrentUserId);

            StateHasChanged();
        }

        public async Task ValidateAuction()
        {
            await AuctionsClient.ValidateAuctionItemAsync(CurrentUserId, Model.Auction.Id);

            StateHasChanged();
        }
    }
   
}
