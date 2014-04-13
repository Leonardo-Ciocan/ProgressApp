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

namespace ProgressApp
{
    public class TileManager
    {

        public async void SaveAndPin(UIElement tile, String ID , TileSize tileSize)
        {
            RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap();
            await renderTargetBitmap.RenderAsync(tile);
            var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();

            StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
            var tileFile = await storageFolder.CreateFileAsync(ID + ".png", CreationCollisionOption.ReplaceExisting);

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
            var tile2 = new SecondaryTile(ID, "a", "b", new Uri("ms-appdata:///local/" + ID + ".png"), tileSize);
            if(tileSize  == TileSize.Wide310x150) tile2.VisualElements.Wide310x150Logo = new Uri("ms-appdata:///local/" + ID + ".png");


            await tile2.RequestCreateAsync();
        }
    }
}
