using Microsoft.AspNetCore.Identity;

namespace Cegeka.Auction.Infrastructure.Identity;

public class ApplicationUser : IdentityUser
{
    public int? LanguageId { get; set; } = null;
    public int? CurrencyId { get; set; } = null;
    public int? TimeZoneId { get; set; } = null;
    public int? DisplaySettingId { get; set; } = null;

    public override bool Equals(object? obj)
    {
        ApplicationUser? user = obj as ApplicationUser;
        return user == null ? false : user.LanguageId == this.LanguageId &&
             user.CurrencyId == this.CurrencyId &&
             user.TimeZoneId == this.TimeZoneId &&
             user.DisplaySettingId == this.DisplaySettingId &&
             user.Id == this.Id &&
             user.UserName == this.UserName &&
             user.PasswordHash == this.PasswordHash;
    }
}
