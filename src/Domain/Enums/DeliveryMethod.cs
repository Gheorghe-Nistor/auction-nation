using System.ComponentModel;

namespace Cegeka.Auction.Domain.Enums;

public enum DeliveryMethod: int
{
    [Description("None")]
    None = 0,
    [Description("Delivery by courier")]
    DeliveryByCourier = 1,
    [Description("Personal pickup")]
    PersonalPickUp = 2
}
