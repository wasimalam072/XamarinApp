using System.Windows.Input;
using Xamarin.Forms;
using ZXing;

namespace XamarinApp
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand ScanResultCommand { get; private set; }
        private bool _doScan = true;
        public MainViewModel()
        {
            ScanResultCommand = new Command(ScanResult);
        }

        private bool _isAnalyzing = true; // start with analysis on
        public bool IsAnalyzing
        {
            get => _isAnalyzing;
            set => SetProperty(ref _isAnalyzing, value);
        }

        private Result _result;
        public Result Result
        {
            get => _result;
            set => SetProperty(ref _result, value);
        }

        private string _barCodeValue;

        public string BarCodeValue
        {
            get { return _barCodeValue; }
            set { _barCodeValue = value; }
        }

        /// <summary>
        /// Unlike your typical button command, this methods is called from the hardware, on a non-UI thread
        /// </summary>
        private void ScanResult()
        {
            //if (!_doScan) // use this non-UI property to block re-entry, as this call comes in on a non-UI thread
            //{
            //    return;
            //}

            //_doScan = false; // doing this immediately, in this thread, to really avoid re-entry

            Device.BeginInvokeOnMainThread(async () =>
            {
                IsAnalyzing = false; // while could set _doScane false earlier, must call the rest from UI thread
                //var barcode = Result.Text;  // not exactly what I do, but same principle.
                BarCodeValue = Result.Text;  // not exactly what I do, but same principle.
                _ = Application.Current.MainPage.DisplayAlert("Bar Code", BarCodeValue, "Cancel");
            });

            // NOTE:  DO NOT set _doScan = true here, because this code runs in separate thread from Main UI thread, BEFORE GoBackWithBarcodeAsync completes.
        }
    }
}
