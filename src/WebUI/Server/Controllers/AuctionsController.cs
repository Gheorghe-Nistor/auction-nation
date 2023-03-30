using Cegeka.Auction.Application.AuctionItems;
using Cegeka.Auction.Application.Common.Services.Data;
using Cegeka.Auction.Application.Users.Queries;
using Cegeka.Auction.Domain.Entities;
using Cegeka.Auction.WebUI.Shared.AccessControl;
using Cegeka.Auction.WebUI.Shared.Auction;
using Cegeka.Auction.WebUI.Shared.Authorization;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Cegeka.Auction.WebUI.Server.Controllers;

// TODO: de mostenit ApiControllerBase - vezi TodoItemsController
[Route("api/[controller]")]
public class AuctionsController : Controller
{
    private readonly IApplicationDbContext _context;
    public AuctionsController(IApplicationDbContext context)
    {
        _context = context;
    }

    // TODO: authorization
    // GET: api/auctions
    [HttpGet]
    public Task<AuctionItemDTO[]> GetAuctionItems()
    {
        return _context.AuctionItems.Select(item => Mapping.DTOFromEntity(item)).ToArrayAsync();
    }


}
