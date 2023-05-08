using Microsoft.AspNetCore.Identity;

namespace Cegeka.Auction.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public int? LanguageId { get; set; } = null;
    public int? CurrencyId { get; set; } = null;
    public int? TimeZoneId { get; set; } = null;
    public int? DisplaySettingId { get; set; } = null;
}
