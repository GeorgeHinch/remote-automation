using SmartThings_Home_Hub__Universal_.Classes;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
using Windows.UI;
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
    public sealed partial class PresencePage : Page
    {
        public PresencePage()
        {
            this.InitializeComponent();
            loadDevices();
        }

        public void loadDevices()
        {
            List<SmartThingsHub> devices = SmartThingsAPI_GetDevices.getDevice("presence");

            foreach (SmartThingsHub sth in devices)
            {
                StackPanel sp = new StackPanel();
                TextBlock tbIcon = new TextBlock();
                TextBlock tbName = new TextBlock();
                TextBlock tbType = new TextBlock();


                #region Create icon textblock
                tbIcon.Text = WebUtility.HtmlDecode("&#57796;");
                if (sth.value == "present")
                {
                    tbIcon.Foreground = new SolidColorBrush(Color.FromArgb(255, 0, 66, 97));
                }
                else { tbIcon.Foreground = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204)); }
                tbIcon.FontFamily = new FontFamily("Segoe MDL2 Assets");
                tbIcon.TextAlignment = TextAlignment.Center;
                tbIcon.FontSize = 96;
                tbIcon.Margin = new Thickness(0, 0, 0, 15);
                #endregion

                #region Create name textblock
                tbName.Text = sth.name;
                tbName.TextAlignment = TextAlignment.Center;
                tbName.Margin = new Thickness(0, 0, 0, 5);
                tbName.Foreground = new SolidColorBrush(Color.FromArgb(255, 89, 89, 89));
                #endregion

                #region Create value textblock
                tbType.Text = sth.value;
                tbType.TextAlignment = TextAlignment.Center;
                tbType.MaxLines = 2;
                tbType.TextWrapping = TextWrapping.Wrap;
                tbType.Foreground = new SolidColorBrush(Color.FromArgb(255, 204, 204, 204));
                #endregion


                #region Add textblocks to stackpanel
                sp.Children.Add(tbIcon);
                sp.Children.Add(tbName);
                sp.Children.Add(tbType);
                #endregion

                #region Add stackpanel to list
                sp.Orientation = Orientation.Vertical;
                sp.HorizontalAlignment = HorizontalAlignment.Center;
                sp.VerticalAlignment = VerticalAlignment.Center;
                sp.Width = 190;
                presence_Stackpanel.Children.Add(sp);
                #endregion
            }
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }
    }
}
