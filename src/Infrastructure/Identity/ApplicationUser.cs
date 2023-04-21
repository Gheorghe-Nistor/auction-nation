using Microsoft.AspNetCore.Identity;

namespace Cegeka.Auction.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public string? Language { get; set; } = string.Empty;
    public string? Currency { get; set; } = string.Empty;
    public string? TimeZone { get; set; } = string.Empty;
    public string? DisplaySetting { get; set; } = string.Empty;
}
