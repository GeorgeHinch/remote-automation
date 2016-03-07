using SmartThings_Home_Hub__Universal_.Classes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Networking.Connectivity;
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
    public sealed partial class NewsPage : Page
    {
        public NewsPage()
        {
            this.InitializeComponent();
            loadTopStories();
        }

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }

        public void loadTopStories()
        {
            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    $"http://api.nytimes.com/svc/topstories/v1/home.json?api-key=a5fdde8f2d6d6ef2ff58ae1d2eeffa3c:4:74641225");
            HttpClient client = new HttpClient();
            //Debug.WriteLine(client);
            if (internet == false)
            {
                
            }
            else
            {
                var response = client.SendAsync(request).Result;
                Debug.WriteLine(response);
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var bytes = Encoding.Unicode.GetBytes(result);
                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(NYT_TopStories));
                        NYT_TopStories stories = (NYT_TopStories)serializer.ReadObject(stream);

                        Debug.WriteLine("Copyright: " + stories.copyright + " |");
                        Debug.WriteLine("Section: " + stories.section + " |");

                        //RadioButton[] radioButtons = new RadioButton[stories.num_results];

                        //for (int i = 0; i < stories.num_results; ++i)
                        //{
                        //    radioButtons[i] = new RadioButton();
                        //    radioButtons[i].Content = current.Text;
                        //    radioButtons[i].Tag = current;
                        //    radioButtons[i].GroupName = WebUtility.HtmlDecode(current.Icon);
                        //    radioButtons[i].IsChecked = current.IsActive;
                        //    radioButtons[i].Style = this.Resources["SplitViewNavButtonStyle"] as Style;
                        //    radioButtons[i].Checked += new RoutedEventHandler(radioButton_Checked);

                        //    this.TopStories_Stackpanel.Children.Add(radioButtons[i]);
                        //}

                        foreach (NYT_Result st_re in stories.results)
                        {
                            //Debug.WriteLine("Title: " + st_re.title + " |");
                            //Debug.WriteLine("Abstract: " + st_re.story_abstract + " |");
                            //Debug.WriteLine("-----");

                            //foreach (Multimedia m in st_re.multimedia)
                            //{
                            //    Debug.WriteLine("Format: " + m.format + " |");
                            //}

                            RadioButton story_rb = new RadioButton();
                            story_rb.Content = st_re.title;
                            story_rb.Tag = st_re.story_abstract;
                            story_rb.Style = this.Resources["NewsViewNavButtonStyle"] as Style;
                            story_rb.Checked += new RoutedEventHandler(radioButton_Checked);

                            this.TopStories_Stackpanel.Children.Add(story_rb);
                        }
                    }
                }
            }
        }

        private void radioButton_Checked(object sender, RoutedEventArgs routedEventArgs)
        {
            RadioButton radioButton = ((RadioButton)sender);
            radioButton.IsChecked = true;
        }
    }
}
