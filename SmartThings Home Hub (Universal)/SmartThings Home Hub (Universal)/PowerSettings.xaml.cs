using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SmartThings_Home_Hub__Universal_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class PowerSettings : Page
    {
        public PowerSettings()
        {
            this.InitializeComponent();
        }

        public void ShutdownComputer(object target, RoutedEventArgs args)
        {
            /*HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"http://localhost:8080/api/control/shutdown");
            HttpClient client = new HttpClient();*/



            Debug.WriteLine("Shutdown");

            ShutdownManager.BeginShutdown(ShutdownKind.Shutdown, new TimeSpan(0));




            /*var response = client.SendAsync(request).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Debug.WriteLine("Shutdown");
            }*/
        }

        public void RebootComputer(object target, RoutedEventArgs args)
        {
            /*HttpRequestMessage request = new HttpRequestMessage(
                HttpMethod.Get,
                $"http://localhost:8080/api/control/reboot");
            HttpClient client = new HttpClient();*/
            Debug.WriteLine("Reboot");

            ShutdownManager.BeginShutdown(ShutdownKind.Shutdown, new TimeSpan(0));

            /*var response = client.SendAsync(request).Result;
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Debug.WriteLine("Shutdown");
            }*/
        }
    }
}
