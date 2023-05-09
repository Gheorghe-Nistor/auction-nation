using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Shared.Auction;
using MediatR;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace Cegeka.Auction.WebUI.Client.Pages.Auction.MyAuctions
{
    public partial class Read
    {
        private TimeSpan TimeLeft { get; set; }

        private string selectedSlide;

        [Parameter]
        public string auctionId { get; set; } = null!;

        [Inject]
        public IAuctionsClient AuctionsClient { get; set; } = null!;
        
        public AuctionItemDetailsVM? Model { get; set; }


        public DeliveryMethod[] Methods = (DeliveryMethod[])Enum.GetValues(typeof(DeliveryMethod));

        protected override async Task OnInitializedAsync()
        {
            Model = await AuctionsClient.GetAuctionAsync(auctionId);
            
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
    }

}
