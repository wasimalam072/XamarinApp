using System;
using Xamarin.Forms;
using ZXing;

namespace XamarinApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainViewModel();

            // https://blog.verslu.is/xamarin/xamarin-forms-xamarin/zxing-android-skipping-frames/
            ScannerView.Options.DelayBetweenAnalyzingFrames = 5; // 5 milliseconds, and lower than the default - weird that it's better
            ScannerView.Options.DelayBetweenContinuousScans = 2000; //2000
            ScannerView.Options.InitialDelayBeforeAnalyzingFrames = 300;
            ScannerView.Options.TryHarder = false;
            ScannerView.Options.TryInverted = false;
            ScannerView.Options.AutoRotate = false;
            ScannerView.Options.UseFrontCameraIfAvailable = false;
            ScannerView.Options.PossibleFormats.Clear();
            ScannerView.Options.PossibleFormats.Add(BarcodeFormat.CODE_128);
            ScannerView.Options.PossibleFormats.Add(BarcodeFormat.QR_CODE);
        }

        private bool _isFirstTorchOnRequest = true;

        private void ScannerOverlay_FlashButtonClicked(Button sender, EventArgs e)
        {
            if (_isFirstTorchOnRequest) // else won't turn on with 1st click
            {
                ScannerView.IsTorchOn = true;
                _isFirstTorchOnRequest = false;
            }
            else
            {
                ScannerView.IsTorchOn = !ScannerView.IsTorchOn;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            ScannerView.IsScanning = true;
        }

        protected override void OnDisappearing()
        {
            base.OnDisappearing();

            ScannerView.IsScanning = false;
        }
    }
}
