using SmartThings_Home_Hub__Universal_.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SmartThings_Home_Hub__Universal_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewsStoryPreview : Page
    {
        public string link;
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            NYT_Result result = e.Parameter as NYT_Result;
            DateTime published = DateTime.Parse(result.updated_date, null, System.Globalization.DateTimeStyles.RoundtripKind);


            IList<Multimedia> previewImage = result.multimedia;
            foreach(Multimedia m in previewImage)
            {
                if(m.format == "superJumbo")
                {
                    Preview_Image.Source = new BitmapImage(new Uri(m.url, UriKind.Absolute));
                }
                else { Preview_Image.Source = null; }
            }

            Preview_Title.Text = result.title;
            if(result.subsection == "")
            {
                Preview_Info.Text = result.section;
            }
            else { Preview_Info.Text = result.section + ", " + result.subsection; }
            Preview_Byline.Text = result.byline;
            Preview_Time.Text = "Published: " + published.ToString("M/d/yyyy, h:mm tt");
            Preview_Abstract.Text = result.story_abstract;

            link = result.url;

            Debug.WriteLine("URL: " + result.url + " |");
        }

        public NewsStoryPreview()
        {
            this.InitializeComponent();
        }

        private void goToLink_Clicked(object sender, RoutedEventArgs e)
        {
            NewsPage.newsPage.StoryWeb_Frame.Visibility = Visibility.Visible;
            NewsPage.newsPage.StoryWeb_View.Visibility = Visibility.Visible;
            NewsPage.newsPage.StoryWeb_View.Source = new Uri(link);
        }
    }
}
