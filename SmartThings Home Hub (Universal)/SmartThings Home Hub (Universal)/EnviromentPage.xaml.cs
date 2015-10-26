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
        }
		
		private void windDirection()
		{
			if ((windDegree >= 348.76 && windDegree <= 360) || (windDegree >= 0 && windDegree <= 11.25))
			{
				windText.Text = windSpeed + " north";
                var rotateAnimation = new DoubleAnimation(0, 0, TimeSpan.FromMilliseconds(200));
                windIcon.RenderTransformOrigin = new Point(0.5, 0.5);
                windIcon.RenderTransform = new RotateTransform();
                var rt = (RotateTransform)windIcon.RenderTransform;
                rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            } else if (windDegree >= 11.26 && windDegree <= 33.75)
			{
				windText.Text = windSpeed + " north by northeast";
                var rotateAnimation = new DoubleAnimation(0, 22, TimeSpan.FromMilliseconds(200));
                windIcon.RenderTransformOrigin = new Point(0.5, 0.5);
                windIcon.RenderTransform = new RotateTransform();
                var rt = (RotateTransform)windIcon.RenderTransform;
                rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            } else if (windDegree >= 33.76 && windDegree <= 56.25)
			{
				windText.Text = windSpeed + " northeast";
                var rotateAnimation = new DoubleAnimation(0, 45, TimeSpan.FromMilliseconds(200));
                windIcon.RenderTransformOrigin = new Point(0.5, 0.5);
                windIcon.RenderTransform = new RotateTransform();
                var rt = (RotateTransform)windIcon.RenderTransform;
                rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            } else if (windDegree >= 56.26 && windDegree <= 78.75)
			{
				windText.Text = windSpeed + " east by northeast";
                var rotateAnimation = new DoubleAnimation(0, 67, TimeSpan.FromMilliseconds(200));
                windIcon.RenderTransformOrigin = new Point(0.5, 0.5);
                windIcon.RenderTransform = new RotateTransform();
                var rt = (RotateTransform)windIcon.RenderTransform;
                rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            } else if (windDegree >= 78.76 && windDegree <= 101.25)
			{
				windText.Text = windSpeed + " east";
                var rotateAnimation = new DoubleAnimation(0, 90, TimeSpan.FromMilliseconds(200));
                windIcon.RenderTransformOrigin = new Point(0.5, 0.5);
                windIcon.RenderTransform = new RotateTransform();
                var rt = (RotateTransform)windIcon.RenderTransform;
                rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            } else if (windDegree >= 101.26 && windDegree <= 123.75)
			{
				windText.text = windSpeed + " east by southeast";
                var rotateAnimation = new DoubleAnimation(0, 112, TimeSpan.FromMilliseconds(200));
                windIcon.RenderTransformOrigin = new Point(0.5, 0.5);
                windIcon.RenderTransform = new RotateTransform();
                var rt = (RotateTransform)windIcon.RenderTransform;
                rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            } else if (windDegree >= 123.76 && windDegree <= 146.25)
			{
				windText.Text = windSpeed + " southeast";
                var rotateAnimation = new DoubleAnimation(0, 135, TimeSpan.FromMilliseconds(200));
                windIcon.RenderTransformOrigin = new Point(0.5, 0.5);
                windIcon.RenderTransform = new RotateTransform();
                var rt = (RotateTransform)windIcon.RenderTransform;
                rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            } else if (windDegree >= 146.26 && windDegree <= 168.75)
			{
				windText.Text = windspeed + " south by southeast";
                var rotateAnimation = new DoubleAnimation(0, 157, TimeSpan.FromMilliseconds(200));
                windIcon.RenderTransformOrigin = new Point(0.5, 0.5);
                windIcon.RenderTransform = new RotateTransform();
                var rt = (RotateTransform)windIcon.RenderTransform;
                rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            } else if (windDegree >= 168.76 && windDegree <= 191.25)
			{
				windText.Text = windSpeed + " south";
                var rotateAnimation = new DoubleAnimation(0, 180, TimeSpan.FromMilliseconds(200));
                windIcon.RenderTransformOrigin = new Point(0.5, 0.5);
                windIcon.RenderTransform = new RotateTransform();
                var rt = (RotateTransform)windIcon.RenderTransform;
                rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            } else if (windDegree >= 191.26 && windDegree <= 213.75)
			{
				windText.Text = windSpeed + " south by southwest";
                var rotateAnimation = new DoubleAnimation(0, 202, TimeSpan.FromMilliseconds(200));
                windIcon.RenderTransformOrigin = new Point(0.5, 0.5);
                windIcon.RenderTransform = new RotateTransform();
                var rt = (RotateTransform)windIcon.RenderTransform;
                rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            } else if (windDegree >= 213.76 && windDegree <= 236.25)
			{
				windText.Text = windSpeed + " southwest";
                var rotateAnimation = new DoubleAnimation(0, 225, TimeSpan.FromMilliseconds(200));
                windIcon.RenderTransformOrigin = new Point(0.5, 0.5);
                windIcon.RenderTransform = new RotateTransform();
                var rt = (RotateTransform)windIcon.RenderTransform;
                rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            } else if (windDegree >= 236.26 && windDegree <= 258.75)
			{
				windText.Text = windSpeed + " west by southwest";
                var rotateAnimation = new DoubleAnimation(0, 247, TimeSpan.FromMilliseconds(200));
                windIcon.RenderTransformOrigin = new Point(0.5, 0.5);
                windIcon.RenderTransform = new RotateTransform();
                var rt = (RotateTransform)windIcon.RenderTransform;
                rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            } else if (windDegree >= 258.76 && windDegree <= 281.25)
			{
				windText.Text = windSpeed + " west";
                var rotateAnimation = new DoubleAnimation(0, 270, TimeSpan.FromMilliseconds(200));
                windIcon.RenderTransformOrigin = new Point(0.5, 0.5);
                windIcon.RenderTransform = new RotateTransform();
                var rt = (RotateTransform)windIcon.RenderTransform;
                rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            } else if (windDegree >= 281.26 && windDegree <= 303.75)
			{
				windText.Text = windSpeed + " west by northwest";
                var rotateAnimation = new DoubleAnimation(0, 292, TimeSpan.FromMilliseconds(200));
                windIcon.RenderTransformOrigin = new Point(0.5, 0.5);
                windIcon.RenderTransform = new RotateTransform();
                var rt = (RotateTransform)windIcon.RenderTransform;
                rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            } else if (windDegree >= 303.76 && windDegree <= 326.25)
			{
				windText.Text = windSpeed + " northwest";
                var rotateAnimation = new DoubleAnimation(0, 315, TimeSpan.FromMilliseconds(200));
                windIcon.RenderTransformOrigin = new Point(0.5, 0.5);
                windIcon.RenderTransform = new RotateTransform();
                var rt = (RotateTransform)windIcon.RenderTransform;
                rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            } else if (windDegree >= 326.26 && windDegree <= 348.75)
			{
				windText.Text = windSpeed + " north by northwest";
                var rotateAnimation = new DoubleAnimation(0, 337, TimeSpan.FromMilliseconds(200));
                windIcon.RenderTransformOrigin = new Point(0.5, 0.5);
                windIcon.RenderTransform = new RotateTransform();
                var rt = (RotateTransform)windIcon.RenderTransform;
                rt.BeginAnimation(RotateTransform.AngleProperty, rotateAnimation);
            } else
			{
				windText.Text = "unknown";
				windText.Text = "";
			}
		}
		
		public int airPressure = 0;
		
		public int humidity = 0;
		
		private void pressure()
		{
			pressureText.Text = airPressure + " hpa";
		}
		
		private void humidity()
		{
			humidityText.Text = humidity + "%";
		}
		
		private void insideTemp()
		{
			
		}
		
		private void outsideTemp()
		{
			
		}
		
		private void thermoDown()
		{
			
		}
		
		private void thermoUp()
		{
			
		}

        private void Home_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(HomePage));
        }
    }
}
