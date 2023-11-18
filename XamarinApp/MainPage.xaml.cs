using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace XamarinApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            try
            {
                //Console.WriteLine("Click here");
                int i = 10, j = 0;
                int result = i / j;
                Analytics.TrackEvent("Button_Clicked");
            }
            catch (Exception Ex)
            {
                Crashes.TrackError(Ex);
            }
        }
    }
}
