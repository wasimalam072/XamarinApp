using Plugin.Media;
using Plugin.Media.Abstractions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Core;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace XamarinApp
{
    public class MainViewModel : BaseViewModel
    {
        public ICommand TakePhoto { get; set; }
        public ICommand PickPhoto { get; set; }
        public ICommand PickVideo { get; set; }
        public MainViewModel()
        {
            TakePhoto = new Command(OnTakePhoto);
            PickPhoto = new Command(OnPickPhoto);
            PickVideo = new Command(OnPickVideo);
        }

        private async void OnPickVideo(object obj)
        {
            try
            {
                if (!CrossMedia.Current.IsPickVideoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert
                        ("Videos Not Supported", ":( Permission not granted to videos.", "OK");
                    return;
                }
                else
                {
                    var file = await CrossMedia.Current.PickVideoAsync();
                    if (file == null)
                    {
                        Console.WriteLine("No File select");
                        return;
                    }
                    else
                    {
                        await LoadPickVideoAsync(file);
                    }
                }
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        private async Task LoadPickVideoAsync(MediaFile fileResult)
        {
            try
            {
                using (var stream = fileResult.GetStream())
                {
                    VideoDisplay = fileResult.AlbumPath;
                    //VideoDisplay = fileResult.Path;
                }
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        private async void OnPickPhoto(object obj)
        {
            try
            {
                if(!CrossMedia.Current.IsPickPhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert
                        ("Storage", ":( No Storgae available.", "OK");
                    return;
                }
                else
                {
                    var file = await CrossMedia.Current.PickPhotoAsync();
                    if (file == null)
                    {
                        Console.WriteLine("No File select");
                        return;
                    }
                    else
                    {
                        await LoadPickPhotoAsync(file);
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Pick Photo Async THREW: {Ex.Message}");
            }
        }

        private async Task LoadPickPhotoAsync(MediaFile fileResult)
        {
            try
            {
                using (var stream = fileResult.GetStream())
                {
                    CaptureImage = ImageSource.FromStream(() =>
                    {
                        var image = fileResult.GetStream();
                        fileResult.Dispose();
                        return image;
                    });
                }
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        private async void OnTakePhoto(object obj)
        {
            try
            {
                if(!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
                {
                    await Application.Current.MainPage.DisplayAlert
                        ("No Camera", ":( No camera available.", "OK");
                    return;
                }
                else
                {
                    var file = await CrossMedia.Current.TakePhotoAsync(new  StoreCameraMediaOptions
                    {
                        Name = string.Format(@"{0}", Guid.NewGuid()) + ".jpg",
                        AllowCropping = true,
                        PhotoSize = PhotoSize.Custom,
                        CustomPhotoSize = 90,
                        CompressionQuality = 92,
                        SaveToAlbum = true
                    });

                    if (file != null)
                    {
                       // await Application.Current.MainPage.DisplayAlert("File Location", file.Path, "OK");
                        await LoadClickPhotoAsync(file);
                    }
                    else
                    {
                        return;
                    }
                }
            }
            catch (Exception Ex)
            {
                Console.WriteLine($"Capture Photo Async THREW: {Ex.Message}");
            }
        }

        private async Task LoadClickPhotoAsync(MediaFile fileResult)
        {
            try
            {
                using (var stream = fileResult.GetStream())
                {
                    CaptureImage = ImageSource.FromStream(() =>
                    {
                        var image = fileResult.GetStream();
                        fileResult.Dispose();
                        return image;
                    });
                }
            }
            catch (Exception Ex)
            {
                throw;
            }
        }

        private ImageSource _captureImage;

        public ImageSource CaptureImage
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

        private MediaSource _videoDisplay;

        public MediaSource VideoDisplay
        {
            get { return _videoDisplay; }
            set
            {
                if (_videoDisplay == value)
                    return;
                _videoDisplay = value;
                OnPropertyChanged();
            }
        }
    }
}
