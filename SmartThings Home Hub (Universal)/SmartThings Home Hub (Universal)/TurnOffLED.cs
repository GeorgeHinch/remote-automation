using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartThings_Home_Hub__Universal_
{
    class TurnOffLED : IColorGenerator
    {
        bool goUp = true;

        public bool generate(Dictionary<byte, StatusLED.RGBVal> vals, byte numLeds)
        {
            if (shouldFillAgain())
            {
                if (goUp)
                {
                    rgbColour[0]++;
                    rgbColour[1]++;
                    rgbColour[2]++;
                }
                else
                {
                    rgbColour[0]--;
                    rgbColour[1]--;
                    rgbColour[2]--;
                }

                if (rgbColour[0] == 128)
                {
                    goUp = false;
                }
                else if (rgbColour[0] == 0)
                {
                    goUp = true;
                }

                this.lastFillTime = ToUnixTimeMS(DateTime.Now);

                for (byte z = 0; z < numLeds; z++)
                {
                    vals[z] = new StatusLED.RGBVal(rgbColour[0], rgbColour[1], rgbColour[2]);
                }

                return true;
            }
            return false;
        }

        private byte numLeds;
        byte[] rgbColour = new byte[3];
        private const double steps = 512;
        private double msPerFill;
        private long lastFillTime;

        /*public FadeGenerator(Boolean together, TimeSpan intervalForRainbow, byte numLeds)
        {
            this.numLeds = numLeds;
            this.msPerFill = Math.Floor((double)((double)intervalForRainbow.TotalMilliseconds / steps));
            this.lastFillTime = 0;
            rgbColour[0] = 0;
            rgbColour[1] = 0;
            rgbColour[2] = 0;
        } */

        private long ToUnixTimeMS(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalMilliseconds);
        }

        private Boolean shouldFillAgain()
        {
            return lastFillTime + msPerFill < this.ToUnixTimeMS(DateTime.Now);
        }
    }
}