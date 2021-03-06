﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SmartThings_Home_Hub__Universal_
{
    class RainbowGenerator : IColorGenerator
    {
        private TimeSpan interval;
        private Boolean together;
        private byte numLeds;
        byte[] rgbColour = new byte[3];
        private const double steps = 765;
        private double msPerFill;
        private long lastFillTime;
        private int incColor =0;
        private int decColor=0;

        public RainbowGenerator(Boolean together, TimeSpan intervalForRainbow, byte numLeds)
        {
            this.together = together;
            this.interval = intervalForRainbow;
            this.numLeds = numLeds;
            Debug.WriteLine("{0} ms interval", intervalForRainbow.TotalMilliseconds);
            Debug.WriteLine("{0} steps", steps);
            this.msPerFill = Math.Floor((double)((double)intervalForRainbow.TotalMilliseconds / steps));
            Debug.WriteLine("TPF is {0}", this.msPerFill);
            this.lastFillTime = 0;
            rgbColour[0] = 255;
            rgbColour[1] = 0;
            rgbColour[2] = 0;
        }

        private long ToUnixTimeMS(DateTime date)
        {
            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return Convert.ToInt64((date - epoch).TotalMilliseconds);
        }

        public bool generate(Dictionary<byte, StatusLED.RGBVal> vals, byte numLeds)
        {
            if (together)
            {
                if (shouldFillAgain())
                {
                    fill();
                    for (byte i = 0; i < numLeds; i++)
                    {
                        vals[i] = new StatusLED.RGBVal(rgbColour[0], rgbColour[1], rgbColour[2]);
                    }
                    this.lastFillTime = this.ToUnixTimeMS(DateTime.Now);
                    return true;

                }
                else
                {

                }
            }

            return false;
        }

        private Boolean shouldFillAgain()
        {
            return lastFillTime + msPerFill < this.ToUnixTimeMS(DateTime.Now);

        }

        int i = 0;
        public void fill()
        {
            incColor = decColor == 2 ? 0 : decColor + 1;

            if (i == 255)
            {
                if (decColor == 2)
                {
                    decColor = 0;
                }
                else
                {
                    decColor++;
                }

                i = 0;
            }
            else
            {
                rgbColour[decColor] -= 1;
                rgbColour[incColor] += 1;
                i++;
            }
            



        }
    }
}
