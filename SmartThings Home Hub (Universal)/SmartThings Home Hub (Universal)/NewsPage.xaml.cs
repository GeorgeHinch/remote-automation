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
        public static NewsPage newsPage;

        public NewsPage()
        {
            this.InitializeComponent();
            newsPage = this;

            loadTopStories();

            RadioButton item1 = (RadioButton)TopStories_Stackpanel.Children[0];
            item1.IsChecked = true;
            StoryWeb_View.Source = new Uri("http://www.nyt.com");
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

                        foreach (NYT_Result st_re in stories.results)
                        {
                            RadioButton story_rb = new RadioButton();
                            story_rb.Content = st_re.title;
                            story_rb.GroupName = st_re.story_abstract;
                            story_rb.Tag = st_re;
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
            NYT_Result item = (NYT_Result)radioButton.Tag;

            if(StoryWeb_Frame.Visibility != Visibility.Collapsed)
            {
                StoryWeb_Frame.Visibility = Visibility.Collapsed;
            }

            foreach (RadioButton rb in TopStories_Stackpanel.Children)
            {
                if(radioButton.Content != rb.Content)
                {
                    rb.IsChecked = false;
                }
            }
            radioButton.IsChecked = true;

            StoryPreview_Frame.Navigate(typeof(NewsStoryPreview), item);
        }

        private void previewReturn_Clicked(object sender, RoutedEventArgs routedEventArgs)
        {
            StoryWeb_Frame.Visibility = Visibility.Collapsed;
        }
    }
}
