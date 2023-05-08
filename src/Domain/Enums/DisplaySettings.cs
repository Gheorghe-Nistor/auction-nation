using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cegeka.Auction.Domain.Enums
{
    public enum DisplaySettings
    {
        [Description("Resolution:1280x1024, Orientation:Landscape")]
        R1280X1240OL,
        [Description("Resolution:1280x1024, Orientation:Portrait")]
        R1280X1240OP,
        [Description("Resolution:1920x1080, Orientation:Landscape")]
        R1920x1080OL,
        [Description("Resolution:1920x1080, Orientation:Portrait")]
        R1920x1080OP
    }
}
