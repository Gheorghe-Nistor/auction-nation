using Microsoft.AspNetCore.Identity;
using Cegeka.Auction.WebUI.Shared.Authorization;

namespace Cegeka.Auction.Infrastructure.Identity;

public class ApplicationRole : IdentityRole
{
    public Permissions Permissions { get; set; }
}
