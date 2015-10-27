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
        }
        public int windDegree = 90;

        public int windSpeed = 0;

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
				windText.Text = windSpeed + " north";
                rotateWindIcon(0);
            } else if (windDegree >= 11.26 && windDegree <= 33.75)
			{
				windText.Text = windSpeed + " north by northeast";
                rotateWindIcon(22.5);
            } else if (windDegree >= 33.76 && windDegree <= 56.25)
			{
				windText.Text = windSpeed + " northeast";
                rotateWindIcon(45);
            } else if (windDegree >= 56.26 && windDegree <= 78.75)
			{
				windText.Text = windSpeed + " east by northeast";
                rotateWindIcon(67.5);
            } else if (windDegree >= 78.76 && windDegree <= 101.25)
			{
				windText.Text = windSpeed + " east";
                rotateWindIcon(90);
            } else if (windDegree >= 101.26 && windDegree <= 123.75)
			{
				windText.Text = windSpeed + " east by southeast";
                rotateWindIcon(112.5);
            } else if (windDegree >= 123.76 && windDegree <= 146.25)
			{
				windText.Text = windSpeed + " southeast";
                rotateWindIcon(135);
            } else if (windDegree >= 146.26 && windDegree <= 168.75)
			{
				windText.Text = windSpeed + " south by southeast";
                rotateWindIcon(157.5);
            } else if (windDegree >= 168.76 && windDegree <= 191.25)
			{
				windText.Text = windSpeed + " south";
                rotateWindIcon(180);
            } else if (windDegree >= 191.26 && windDegree <= 213.75)
			{
				windText.Text = windSpeed + " south by southwest";
                rotateWindIcon(202.5);
            } else if (windDegree >= 213.76 && windDegree <= 236.25)
			{
				windText.Text = windSpeed + " southwest";
                rotateWindIcon(225);
            } else if (windDegree >= 236.26 && windDegree <= 258.75)
			{
				windText.Text = windSpeed + " west by southwest";
                rotateWindIcon(247.5);
            } else if (windDegree >= 258.76 && windDegree <= 281.25)
			{
				windText.Text = windSpeed + " west";
                rotateWindIcon(270);
            } else if (windDegree >= 281.26 && windDegree <= 303.75)
			{
				windText.Text = windSpeed + " west by northwest";
                rotateWindIcon(292.5);
            } else if (windDegree >= 303.76 && windDegree <= 326.25)
			{
				windText.Text = windSpeed + " northwest";
                rotateWindIcon(315);
            } else if (windDegree >= 326.26 && windDegree <= 348.75)
			{
				windText.Text = windSpeed + " north by northwest";
                rotateWindIcon(337.5);
            }
            else
			{
				windText.Text = "unknown";
				windText.Text = "";
			}
		}
		
		public int airPressure = 0;
		
		public int humidity = 0;
		
		private void getPressure()
		{
            pressureText.Text = airPressure + " hpa";
		}
		
		private void getHumidity()
		{
			humidityText.Text = humidity + "%";
		}
		
		private void insideTemp()
		{
			
		}
		
		private void outsideTemp()
		{
			
		}
		
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
