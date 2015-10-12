using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartThings_Home_Hub__Universal_
{
    interface IColorGenerator
    {
        Boolean generate(Dictionary<byte, StatusLED.RGBVal> vals, byte numLeds);
    }
}
