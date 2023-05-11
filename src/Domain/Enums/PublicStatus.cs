using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Tracing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Domain.Enums;

public enum PublicStatus: int
{
    [Description("None")]
    None = 0,
    [Description("Active")]
    Active = 1,
    [Description("Closed")]
    Closed = 2
}
