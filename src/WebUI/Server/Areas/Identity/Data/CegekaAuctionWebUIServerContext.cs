using Cegeka.Auction.Infrastructure.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Cegeka.Auction.WebUI.Server.Data;

public class CegekaAuctionWebUIServerContext : IdentityDbContext<ApplicationUser>
{
    public CegekaAuctionWebUIServerContext(DbContextOptions<CegekaAuctionWebUIServerContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
