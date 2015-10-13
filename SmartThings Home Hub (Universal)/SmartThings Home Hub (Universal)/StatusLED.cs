using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.Devices.Gpio;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace SmartThings_Home_Hub__Universal_
{

    public class StatusLED
    {

        private byte clockPin;
        private byte dataPin;

        private Dictionary<byte, RGBVal> ledVals;
        private Dictionary<byte, RGBVal> generatedLedVals;

        private DispatcherTimer timer;
        private GpioController gpio;
        private GpioPin gpClockPin;
        private GpioPin gpDataPin;

        private GpioOpenStatus gpClockStatus;
        private GpioOpenStatus gpDataStatus;

        private Boolean hasUpdate;

        private Boolean manualMode;

        private byte numLeds;

        private IColorGenerator colorGenerator;


        public StatusLED(byte clock_pin, byte data_pin, byte num_leds)
        {
          if (gpio == null)
            {
                Debug.WriteLine("There is no GPIO controller on this device.");
                return;
            } else
            {
              this.clockPin = clock_pin;
              this.dataPin = data_pin;
              this.numLeds = num_leds;
              this.ledVals = new Dictionary<byte, RGBVal>();
              this.generatedLedVals = new Dictionary<byte, RGBVal>();
              this.timer = new DispatcherTimer();
              this.gpio = GpioController.GetDefault();
              bool clockOpen = gpio.TryOpenPin(this.clockPin, GpioSharingMode.Exclusive, out gpClockPin, out gpClockStatus);
              bool dataOpen = gpio.TryOpenPin(this.dataPin, GpioSharingMode.Exclusive, out gpDataPin, out gpDataStatus);
              gpClockPin.SetDriveMode(GpioPinDriveMode.Output);
              gpDataPin.SetDriveMode(GpioPinDriveMode.Output);
              if (!dataOpen || !clockOpen)
              {
                  throw new Exception("Failed to open required pins.");
              }
              for (byte i = 0; i < this.numLeds; i++)
              {
                  this.ledVals[i] = new RGBVal(0, 0, 0);
                  this.generatedLedVals[i] = new RGBVal(0, 0, 0);
              }
              hasUpdate = true;
              manualMode = true;
              this.timer.Interval = TimeSpan.FromMilliseconds(100);
              this.timer.Tick += Timer_Tick;
              this.timer.Start();
              Debug.WriteLine("Started status led thing.");
            }
        }

        private void Timer_Tick(object sender, object e)
        {
            //Debug.WriteLine("Tick");
            //Ticks every 20 ms
            //Send the shit from the map
            lock (this)
            {
                if (!manualMode && colorGenerator != null)
                {
                    this.hasUpdate = colorGenerator.generate(generatedLedVals, this.numLeds);
                }
                if (this.hasUpdate == true)
                {
                    this.setColorRGB();
                    this.hasUpdate = false;
                }
            }
            //Debug.WriteLine("tick stop");
        }


        private void clock()
        {
            this.gpClockPin.Write(GpioPinValue.Low);
            NOP(20);
            this.gpClockPin.Write(GpioPinValue.High);
            NOP(20);
        }


        private void NOP(long durationTicks)
        {
            Stopwatch sw = new Stopwatch();

            sw.Start();

            while (sw.ElapsedTicks < durationTicks)
            {

            }
        }


        private void sendByte(byte b)
        {
            //Debug.WriteLine(b);
            // "Send one bit at a time, starting with the MSB"
            for (byte i = 0; i < 8; i++)
            {
                // If MSB is 1, write one and clock it, else write 0 and clock
                if ((b & 0x80) != 0)
                {
                    this.gpDataPin.Write(GpioPinValue.High);
                }
                else
                {
                    this.gpDataPin.Write(GpioPinValue.Low);
                }
                this.clock();
                b = (byte)(b << 1);
            }
        }

        private void sendColor(byte red, byte green, byte blue)
        {
            //Debug.WriteLine(String.Format("{0},{1},{2}", red, green, blue));
            //"Start by sending a byte with the format '1 1 /B7 /B6 /G7 /G6 /R7 /R6' "
            //#prefix = B11000000
            byte prefix = Convert.ToByte("11000000", 2);
            if ((blue & 0x80) == 0) prefix |= Convert.ToByte("00100000", 2);
            if ((blue & 0x40) == 0) prefix |= Convert.ToByte("00010000", 2);
            if ((green & 0x80) == 0) prefix |= Convert.ToByte("00001000", 2);
            if ((green & 0x40) == 0) prefix |= Convert.ToByte("00000100", 2);
            if ((red & 0x80) == 0) prefix |= Convert.ToByte("00000010", 2);
            if ((red & 0x40) == 0) prefix |= Convert.ToByte("00000001", 2);
            sendByte(prefix);


            //# Now must send the 3 colors
            this.sendByte(blue);
            this.sendByte(green);
            this.sendByte(red);

        }

        private void setColorRGB()
        {

            // Send data frame prefix (32x '0')
            this.sendByte(0x00);
            this.sendByte(0x00);
            this.sendByte(0x00);
            this.sendByte(0x00);

            //# Send color data for each one of the leds

            for (byte i = 0; i < this.numLeds; i++)
            {
                RGBVal something;
                if (manualMode)
                {
                    something = this.ledVals[i];
                }
                else
                {
                    something = this.generatedLedVals[i];
                }
                //Debug.WriteLine(String.Format("{0},{1},{2}", something.getR(), something.getG(), something.getB()));
                this.sendColor(something.getR(), something.getG(), something.getB());
            }

            // # Terminate data frame (32x "0")
            this.sendByte(0x00);
            this.sendByte(0x00);
            this.sendByte(0x00);
            this.sendByte(0x00);
        }

        public void setColorAll(byte r, byte g, byte b)
        {
            for (byte i = 0; i < this.numLeds; i++)
            {
                this.setColor(i, r, g, b);
            }
        }


        public void setColor(byte whihcLED, byte r, byte g, byte b)
        {
            lock (this)
            {
                ledVals[whihcLED] = new RGBVal(r, g, b);
                hasUpdate = true;
                manualMode = true;
            }
        }

        public class RGBVal
        {
            byte r;
            byte g;
            byte b;

            public RGBVal(byte r, byte g, byte b)
            {
                this.r = r;
                this.g = g;
                this.b = b;
            }

            public byte getR()
            {
                return r;
            }

            public byte getG()
            {
                return g;
            }

            public byte getB()
            {
                return b;
            }
        }


        public void rainbowTogether()
        {
            this.manualMode = false;
            this.colorGenerator = new RainbowGenerator(true, TimeSpan.FromSeconds(30), numLeds);
        }
    }
}
