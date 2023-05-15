namespace Cegeka.Auction.WebUI.Shared.AccessControl;

public class UserDto
{
    public UserDto() : this(string.Empty, string.Empty, string.Empty, null) { }

    public UserDto(string id, string userName, string email,int? currencyId)
    {
        Id = id;
        UserName = userName;
        Email = email;
        CurrencyId = currencyId;
    }

    public string Id { get; set; }

    public string UserName { get; set; }

    public string Email { get; set; }
    public int? LanguageId { get; set; } = null;
    public int? CurrencyId { get; set; } = null;
    public int? TimeZoneId { get; set; } = null;
    public int? DisplaySettingId { get; set; } = null;

    public List<string> Roles { get; set; } = new();
}
