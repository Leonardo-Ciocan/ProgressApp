using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Graphics.Display;
using Windows.Graphics.Imaging;
using Windows.Storage;
using Windows.UI;
using Windows.UI.StartScreen;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ProgressApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DetailsPage : Page
    {
        public DetailsPage()
        {
            this.InitializeComponent();
            Windows.Phone.UI.Input.HardwareButtons.BackPressed += (a, b) =>
            {
                if (Frame.CanGoBack) Frame.GoBack();
                b.Handled = true;
            };
            this.Loaded += DetailsPage_Loaded;
        }

        void DetailsPage_Loaded(object sender, RoutedEventArgs e)
        {
            itemControl.UserEditable = true;
        }

        ProgressItem self;
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = e.Parameter;
            self = DataContext as ProgressItem;
            StatusBar.GetForCurrentView().BackgroundColor = (e.Parameter as ProgressItem).Color;
            StatusBar.GetForCurrentView().BackgroundOpacity = 1;
            StatusBar.GetForCurrentView().ForegroundColor = Colors.White;

            var uriLogo = new Uri("ms-appx:///Assets/Logo.scale-240.png");
            //var uriSmallLogo = new Uri("ms-appx:///images/smallLogoSecondaryTile-sdk.png");



           



        }

        async void PinTile()
        {
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap();
            await renderTargetBitmap.RenderAsync(tile);
            var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();

                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                var tileFile = await storageFolder.CreateFileAsync(self.ID + ".png", CreationCollisionOption.ReplaceExisting);

                // Encode the image to the selected file on disk
                using (var fileStream = await tileFile.OpenAsync(FileAccessMode.ReadWrite))
                {
                    var encoder = await BitmapEncoder.CreateAsync(BitmapEncoder.PngEncoderId, fileStream);

                    encoder.SetPixelData(
                        BitmapPixelFormat.Bgra8,
                        BitmapAlphaMode.Straight,
                        (uint)renderTargetBitmap.PixelWidth,
                        (uint)renderTargetBitmap.PixelHeight,
                        DisplayInformation.GetForCurrentView().LogicalDpi,
                        DisplayInformation.GetForCurrentView().LogicalDpi,
                        pixelBuffer.ToArray());

                    await encoder.FlushAsync();
                }
                var tile2 = new SecondaryTile(self.ID, "a", "b", new Uri("ms-appdata:///local/"+self.ID+".png"), TileSize.Wide310x150);
                tile2.VisualElements.Wide310x150Logo = new Uri("ms-appdata:///local/" + self.ID + ".png");
                
            
             await tile2.RequestCreateAsync();
        }

        private void PinClicked(object sender, RoutedEventArgs e)
        {
            PinTile();
        }
    }
}
