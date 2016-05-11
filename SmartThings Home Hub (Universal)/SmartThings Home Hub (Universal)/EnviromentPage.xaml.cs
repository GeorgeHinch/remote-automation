using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using System.Diagnostics;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Net;
using System.Text;
using SmartThings_Home_Hub__Universal_.Classes;
using Windows.Networking.Connectivity;
using static SmartThings_Home_Hub__Universal_.Classes.OWM_WeatherStatus;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace SmartThings_Home_Hub__Universal_
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EnviromentPage : Page
    {
        public EnviromentPage()
        {
            this.InitializeComponent();
            windDirection();
            openWeatherSerializer();
            insideTemp();
        }

        public int windDegree = 0;

        public string windSpeed = "0";

        public void openWeatherSerializer()
        {
            WeatherDetails weatherDetails = OWM_GetWeather.getSingleCity();

            if (weatherDetails != null)
            {
                outsideTempBox.Text = Math.Round(weatherDetails.main.temp) + "°";
                pressureText.Text = Math.Round(weatherDetails.main.pressure) + " hPa";
                humidityText.Text = weatherDetails.main.humidity + "%";

                windDegree = (int)weatherDetails.wind.deg;
                windSpeed = weatherDetails.wind.speed.ToString();

                windDirection();
            }
            else
            {
                outsideTempBox.Text = "err";
                pressureText.Text = "err";
                humidityText.Text = "err";
            }
        }

        private void rotateWindIcon(double angle)
        {
            windIcon.RenderTransformOrigin = new Point(0.5, 0.5);
            windIcon.RenderTransform = new RotateTransform() { Angle = angle, CenterX = .5, CenterY = .5 };
            windIcon.UpdateLayout();
        }
		
		private void windDirection()
		{
			if ((windDegree >= 348.76 && windDegree <= 360) || (windDegree >= 0 && windDegree <= 11.25))
			{
				windText.Text = windSpeed + " mph north";
                rotateWindIcon(0);
            } else if (windDegree >= 11.26 && windDegree <= 33.75)
			{
				windText.Text = windSpeed + " mph north by northeast";
                rotateWindIcon(22.5);
            } else if (windDegree >= 33.76 && windDegree <= 56.25)
			{
				windText.Text = windSpeed + " mph northeast";
                rotateWindIcon(45);
            } else if (windDegree >= 56.26 && windDegree <= 78.75)
			{
				windText.Text = windSpeed + " mph east by northeast";
                rotateWindIcon(67.5);
            } else if (windDegree >= 78.76 && windDegree <= 101.25)
			{
				windText.Text = windSpeed + " mph east";
                rotateWindIcon(90);
            } else if (windDegree >= 101.26 && windDegree <= 123.75)
			{
				windText.Text = windSpeed + " mph east by southeast";
                rotateWindIcon(112.5);
            } else if (windDegree >= 123.76 && windDegree <= 146.25)
			{
				windText.Text = windSpeed + " mph southeast";
                rotateWindIcon(135);
            } else if (windDegree >= 146.26 && windDegree <= 168.75)
			{
				windText.Text = windSpeed + " mph south by southeast";
                rotateWindIcon(157.5);
            } else if (windDegree >= 168.76 && windDegree <= 191.25)
			{
				windText.Text = windSpeed + " mph south";
                rotateWindIcon(180);
            } else if (windDegree >= 191.26 && windDegree <= 213.75)
			{
				windText.Text = windSpeed + " mph south by southwest";
                rotateWindIcon(202.5);
            } else if (windDegree >= 213.76 && windDegree <= 236.25)
			{
				windText.Text = windSpeed + " mph southwest";
                rotateWindIcon(225);
            } else if (windDegree >= 236.26 && windDegree <= 258.75)
			{
				windText.Text = windSpeed + " mph west by southwest";
                rotateWindIcon(247.5);
            } else if (windDegree >= 258.76 && windDegree <= 281.25)
			{
				windText.Text = windSpeed + " mph west";
                rotateWindIcon(270);
            } else if (windDegree >= 281.26 && windDegree <= 303.75)
			{
				windText.Text = windSpeed + " mph west by northwest";
                rotateWindIcon(292.5);
            } else if (windDegree >= 303.76 && windDegree <= 326.25)
			{
				windText.Text = windSpeed + " mph northwest";
                rotateWindIcon(315);
            } else if (windDegree >= 326.26 && windDegree <= 348.75)
			{
				windText.Text = windSpeed + " mph north by northwest";
                rotateWindIcon(337.5);
            }
            else
			{
				windText.Text = "unknown";
				windText.Text = "";
			}
		}
		
		private void insideTemp()
		{
            string app = getApp();
            string token = getToken();

            ConnectionProfile connections = NetworkInformation.GetInternetConnectionProfile();
            bool internet = connections != null && connections.GetNetworkConnectivityLevel() == NetworkConnectivityLevel.InternetAccess;

            string rqstMsg = "https://graph.api.smartthings.com/api/smartapps/installations/" + app + "/data?access_token=" + token;

            HttpRequestMessage request = new HttpRequestMessage(
                    HttpMethod.Get,
                    rqstMsg);
            HttpClient client = new HttpClient();
            if (internet != false)
            {
                var response = client.SendAsync(request).Result;
                if (response.StatusCode == HttpStatusCode.OK)
                {
                    var result = response.Content.ReadAsStringAsync().Result;
                    var bytes = Encoding.Unicode.GetBytes(result);
                    using (MemoryStream stream = new MemoryStream(bytes))
                    {
                        var serializer = new DataContractJsonSerializer(typeof(SmartThingsHub[]));
                        SmartThingsHub[] devices = (SmartThingsHub[])serializer.ReadObject(stream);

                        #region Creates stackpanel from ST JSON
                        foreach (SmartThingsHub sth in devices)
                        {
                            if (sth.tile == "device" && sth.type == "temperature")
                            {
                                #region Updates inside temp
                                insideTempBox.Text = sth.value;
                                #endregion
                            }
                        }
                        #endregion
                    }
                }
            }
        }

        #region Gets the SmartThings app ID
        public string getApp()
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            string app = "";

            /* Load SmartThings App ID */
            if (roamingSettings.Values["stAppID"] == null)
            {
                this.Frame.Navigate(typeof(SettingsPage));
            }
            else
            {
                app = roamingSettings.Values["stAppID"].ToString();
            }

            return app;
        }
        #endregion

        #region Gets the SmartThings app token
        public string getToken()
        {
            var roamingSettings = Windows.Storage.ApplicationData.Current.RoamingSettings;
            string token = "";

            /* Load SmartThings Access Token */
            if (roamingSettings.Values["stToken"] == null)
            {
                this.Frame.Navigate(typeof(SettingsPage));
            }
            else
            {
                token = roamingSettings.Values["stToken"].ToString();
            }

            return token;
        }
        #endregion

        private void thermoDown(object sender, RoutedEventArgs e)
		{
			
		}
		
		private void thermoUp(object sender, RoutedEventArgs e)
		{
			
		}

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }
    }
}
