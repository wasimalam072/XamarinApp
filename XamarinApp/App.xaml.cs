using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.AppCenter;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace XamarinApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            AppCenterCrashMethod();
        }

        protected override void OnSleep()
        {

        }

        protected override void OnResume()
        {
            AppCenterCrashMethod();
        }

        protected void AppCenterCrashMethod()
        {
            AppCenter.Start("android=55578713-d2b3-4c4c-abb4-a0f2830da90c;" +
                  "ios=4c82566e-b3a5-4197-b96b-5485d831983f;", typeof(Analytics), typeof(Crashes));
        }
    }
}
