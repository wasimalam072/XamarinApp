using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinApp
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand TakePhoto { get; }
        public ICommand PickPhoto { get; }
        public ICommand RemovePhoto { get; }
        public MainViewModel()
        {
            TakePhoto = new Command(OnTakePhoto);
            PickPhoto = new Command(OnPickPhoto);
            RemovePhoto = new Command(OnRemovePhoto);
        }

        private async void OnPickPhoto(object obj)
        {
            try
            {
                var fileResult = await MediaPicker.PickPhotoAsync();
                if (fileResult != null)
                {
                    await LoadPickPhotoAsync(fileResult);
                }
                else
                {
                    return;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        private async Task LoadPickPhotoAsync(FileResult fileResult)
        {
            var newFile = Path.Combine(FileSystem.CacheDirectory, fileResult.FileName);
            using (var stream = await fileResult.OpenReadAsync())
            {
                using (var newStream = File.OpenWrite(newFile))
                {
                    await stream.CopyToAsync(newStream);
                }
            }
            CaptureImage = newFile;
        }

        private async void OnTakePhoto(object obj)
        {
            try
            {
                var fileResult = await MediaPicker.CapturePhotoAsync();
                if (fileResult != null)
                {
                    await LoadCapturePhotoAsync(fileResult);
                }
                else
                {
                    return;
                }
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }
        }

        private async Task LoadCapturePhotoAsync(FileResult fileResult)
        {
            var newFile = Path.Combine(FileSystem.CacheDirectory, fileResult.FileName);
            using(var stream = await fileResult.OpenReadAsync())
            {
                using(var newStream = File.OpenWrite(newFile))
                {
                    await stream.CopyToAsync(newStream);
                }
            }
            CaptureImage = newFile;
        }

        private void OnRemovePhoto(object obj)
        {
            if (CaptureImage != null)
            {
                CaptureImage = null;
            }
            else
            {
                return;
            }
        }

        private string _captureImage;

        public string CaptureImage
        {
            get { return _captureImage; }
            set 
            {
                if (_captureImage == value)
                    return;
                _captureImage = value;
                OnPropertyChanged();
            }
        }

    }
}
