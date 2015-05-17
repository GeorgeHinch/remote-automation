using System;
using System.Collections.Generic;
using System.Linq;
using System.Timers;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Runtime.InteropServices;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace SmartThings_Hub
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        NetworkStatus networkStatuserrrrr;
        ResourceDictionary dic;

        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;
            timer.Start();
            networkStatuserrrrr = new NetworkStatus();
            //dic = this.Resources.MergedDictionaries[0];
        }

        void timer_Tick(object sender, EventArgs e)
        {
            LockTime.Content = DateTime.Now.ToString("h" + " " + "mm");
            LockDate.Content = DateTime.Now.ToString("dddd, " + "MMMM dd" + ", " + "yyyy");
            LockPower.Content = MainWindow.PowerLineStatus;
            NetworkStatusResult currentStatus = networkStatuserrrrr.refresh();
            //Console.WriteLine(currentStatus.Ssid);
            switch (currentStatus.SigStrength){
                case NetworkResultSignalStrength.ONE:
                    //sigStrengthIcon.Source = ((Image)dic["ic_signal_wifi_statusbar_4_bar_50px"]).Source;
                    break;
                
                    
            }
        }

        public static PowerLineStatus PowerLineStatus
        {
            get;
        }
    }
}






/// http://www.wpf-tutorial.com/misc/dispatchertimer/
/// http://www.thecodingguys.net/tutorials/csharp/csharp-dates-and-times