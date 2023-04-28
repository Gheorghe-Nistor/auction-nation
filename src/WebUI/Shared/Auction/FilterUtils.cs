using Cegeka.Auction.Domain.Enums;

namespace Cegeka.Auction.WebUI.Shared.Auction;

public class FilterUtils
{
    public Category Category { get; set; } = Category.None;
    public PublicStatus Status { get; set; } = PublicStatus.None;

    public bool IsEmpty()
    {
        return Category == Category.None && Status == PublicStatus.None;
    }
}
