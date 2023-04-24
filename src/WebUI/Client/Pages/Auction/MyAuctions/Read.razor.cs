using Cegeka.Auction.Domain.Enums;
using Cegeka.Auction.WebUI.Client.Pages.Auction.Bids;
using Cegeka.Auction.WebUI.Shared.Auction;
using Cegeka.Auction.WebUI.Shared.Bid;
using MediatR;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Cegeka.Auction.WebUI.Client.Pages.Auction.MyAuctions
{
    public partial class Read
    {
        private TimeSpan TimeLeft { get; set; }

        [Parameter]
        public string auctionId { get; set; } = null!;

        //[Inject]
        //public IHttpContextAccessor HttpContextAccessor { get; set; } = null!;

        [Inject]
        public IAuctionsClient AuctionsClient { get; set; } = null!;

        public AuctionItemDetailsVM? Model { get; set; }

        public DeliveryMethod[] Methods = (DeliveryMethod[])Enum.GetValues(typeof(DeliveryMethod));

        public bool BuyItNowAvailable { get; set; } = false;

        private BidDialog _bidDialog { get; set; }

        protected async Task PlaceBidForItem()
        {
            _bidDialog.Show();
        }

        protected async void DialogAddForPlaceBid(bool arg)
        {
            if (arg)
            {
                BidDTO bid = new BidDTO() { Amount = _bidDialog.Amount };
                Console.WriteLine(bid.Amount);
            }
        }

        protected override async Task OnInitializedAsync()
        {
            Model = await AuctionsClient.GetAuctionAsync(auctionId);

            var currentUser = "";
            //var currentUser = HttpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;

            if (DateTime.Now < Model.Auction.StartDate && currentUser != Model.Auction.CreatedBy)
            {
                BuyItNowAvailable = true;
            }
            var endDate = Model.Auction.EndDate;
            var timer = new System.Timers.Timer(1000);
            timer.Elapsed += (sender, e) =>
            {
                TimeLeft = endDate - DateTime.Now;
                StateHasChanged();
            };
            timer.Start();
        }


    }
   
}
