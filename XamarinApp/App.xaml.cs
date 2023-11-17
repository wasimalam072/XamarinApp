using Plugin.FirebasePushNotification;
using System;
using Xamarin.Forms;

namespace XamarinApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();

            FirebasePushNotificationMethod();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        protected void FirebasePushNotificationMethod()
        {
            /// <summary>
            /// Event triggered when token is refreshed
            /// </summary>
            CrossFirebasePushNotification.Current.OnTokenRefresh += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine($"TOKEN : {p.Token}");
            };

            /// <summary>
            /// Event triggered when a notification is received
            /// </summary>
            CrossFirebasePushNotification.Current.OnNotificationReceived += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Received");
            };

            /// <summary>
            /// Event triggered when a notification is opened
            /// </summary>
            CrossFirebasePushNotification.Current.OnNotificationOpened += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Opened");
                foreach (var data in p.Data)
                {
                    System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                }
            };

            /// <summary>
            /// Event triggered when there's an error
            /// </summary>
            CrossFirebasePushNotification.Current.OnNotificationAction += (s, p) =>
            {
                System.Diagnostics.Debug.WriteLine("Action");

                if (!string.IsNullOrEmpty(p.Identifier))
                {
                    System.Diagnostics.Debug.WriteLine($"ActionId: {p.Identifier}");
                    foreach (var data in p.Data)
                    {
                        System.Diagnostics.Debug.WriteLine($"{data.Key} : {data.Value}");
                    }

                }
            };

            /// <summary>
            /// Event triggered when Notification Delete
            /// </summary>
            CrossFirebasePushNotification.Current.OnNotificationDeleted += (s, p) => 
            {
                System.Diagnostics.Debug.WriteLine("Deleted");
            }; 

        }
    }
}
