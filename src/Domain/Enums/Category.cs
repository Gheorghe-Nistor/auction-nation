using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Domain.Enums;

public enum Category: int
{
    [Description("None")]
    None = 0,
    [Description("Books")]
    Books = 1,
    [Description("Music")]
    Music = 2,
    [Description("Movies")]
    Movies = 3,
    [Description("Beauty")]
    Beauty = 4,
    [Description("Clothing")]
    Clothing = 5,
    [Description("Accessories")]
    Accessories = 6,
    [Description("Electronics")]
    Electronics = 7,
    [Description("Vehicles")]
    Vehicles = 8,
    [Description("Toys")]
    Toys = 9,
    [Description("Sport")]
    Sport = 10,
    [Description("Home & Gardening")]
    HomeAndGardening = 11,
    [Description("Collectibles & Memorabilia")]
    CollectiblesAndMemorabilia = 12
}
