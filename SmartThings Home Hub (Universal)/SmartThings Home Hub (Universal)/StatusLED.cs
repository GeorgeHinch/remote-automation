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

    class StatusLED
    {

        public static void LEDTimer()
        {
            InitGPIO();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromMilliseconds(500);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private static void Timer_Tick(object sender, object e)
        {
            sendColor(0,255,0);
        }

        
        private static void InitGPIO()
        {
            var gpio = GpioController.GetDefault();

            // Show an error if there is no GPIO controller
            if (gpio == null)
            {
                whitewire = null;
                yellowwire = null;
                Debug.WriteLine("There is no GPIO controller on this device.");
                //return;
            }

            whitewire = gpio.OpenPin(WHITE_WIRE);
            yellowwire = gpio.OpenPin(YELLOW_WIRE);

            // Show an error if the pin wasn't initialized properly
            if (whitewire == null || yellowwire == null)
            {
                Debug.WriteLine("There were problems initializing the GPIO red/blue/green pin.");
                return;
            }

            whitewire.Write(GpioPinValue.High);
            whitewire.SetDriveMode(GpioPinDriveMode.Output);
            yellowwire.Write(GpioPinValue.High);
            yellowwire.SetDriveMode(GpioPinDriveMode.Output);

            Debug.WriteLine("GPIO blue/red/green pin initialized correctly.");
        }

        private static void sendBit(bool Bit)
        {
            if (Bit == true)
            {
                yellowwire.Write(GpioPinValue.High);
            }
            else
            {
                yellowwire.Write(GpioPinValue.Low);
            }
            whitewire.Write(GpioPinValue.High);
            Task.Delay(TimeSpan.FromMilliseconds(2)).RunSynchronously();
            whitewire.Write(GpioPinValue.Low);
            Task.Delay(TimeSpan.FromMilliseconds(2)).RunSynchronously();
        }

        private static void sendByte(int number)
        {
            for (int i=0; i<8; i++)
            {
                //1   = 00000001
                //7   = 00000111
                //1&7 = 00000001
                sendBit((number & 1) == 1);
                
                //7    = 00000111
                //7>>1 = 00000011
                number = number >> 1;
            }
        }

        private static void sendColor(int r, int g, int b)
        {
            //sendByte(0);
            sendByte(r);
            sendByte(g);
            sendByte(b);
        }

        private const int WHITE_WIRE = 8;
        private const int YELLOW_WIRE = 10;
        private static GpioPin whitewire;
        private static GpioPin yellowwire;
        private static DispatcherTimer timer;
    }
}
