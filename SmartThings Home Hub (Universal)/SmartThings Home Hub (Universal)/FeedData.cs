using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Web.Syndication;

namespace SmartThings_Home_Hub__Universal_
{
    public class FeedData // Holds info for a single blog feed, including a list of blog posts (FeedItem)
    {
        public string Title { get; set; }

        public string Description { get; set; }

        public DateTime PubDate { get; set; }

        private List<FeedItem> _Items = new List<FeedItem>();
        public List<FeedItem> Items
        {
            get
            {
                return this._Items;
            }
        }
    }

    public class FeedItem //holds info for a single blog post
    {
        public string Title { get; set; }

        public string Author { get; set; }

        public string[] Categories { get; set; }

        public string Description { get; set; }

        public DateTime PubDate { get; set; }

        public Uri Link { get; set; }

        public Uri CommentsLink { get; set; }

        public string Summary { get; set; }

        public string Brief { get; set; }

        public string CategoriesAppended { get; set; }
    }

    // Holds a collection of blog feeds (FeedData), and contains methods needed to
    // retreive the feeds.
    public class FeedDataSource
    {
        public static FeedDataSource GlobalDataSource = new FeedDataSource();

        private ObservableCollection<FeedData> _Feeds = new ObservableCollection<FeedData>();
        public ObservableCollection<FeedData> Feeds
        {
            get
            {
                return this._Feeds;
            }
        }

        public async Task GetFeedsAsync()
        {
            Task<FeedData> feed1 = GetFeedAsync("http://violabazanye.wordpress.com/feed/");

            this.Feeds.Add(await feed1);
        }

        private async Task<FeedData> GetFeedAsync(string feedUriString)
        {
            SyndicationClient client = new SyndicationClient();
            Uri feedUri = new Uri(feedUriString);

            try
            {
                SyndicationFeed feed = await client.RetrieveFeedAsync(feedUri);

                // This code is executed after RetrieveFeedAsync returns the SyndicationFeed.
                // Process it and copy the data we want into our FeedData and FeedItem classes.
                FeedData feedData = new FeedData();
                feedData.Title = feed.Title.Text;
                if (feed.Subtitle != null && feed.Subtitle.Text != null)
                {
                    feedData.Description = feed.Subtitle.Text;
                }

                // Use the date of the latest post as the last updated date.
                feedData.PubDate = feed.Items[0].PublishedDate.DateTime;

                foreach (SyndicationItem item in feed.Items)
                {
                    FeedItem feedItem = new FeedItem();

                    feedItem.Title = item.Title.Text;
                    feedItem.PubDate = item.PublishedDate.DateTime;
                    feedItem.Author = item.Authors[0].NodeValue;
                    feedItem.Link = item.Links[0].Uri;
                    feedItem.Categories = item.Categories.Select(x => x.NodeValue).ToArray();
                    feedItem.Summary = Windows.Data.Html.HtmlUtilities.ConvertToText(item.Summary.Text);
                    feedItem.Brief = feedItem.Summary.Substring(0, 52) + "...";
                    feedItem.CategoriesAppended = string.Join(", ", feedItem.Categories);
                    feedData.Items.Add(feedItem);
                }
                return feedData;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
