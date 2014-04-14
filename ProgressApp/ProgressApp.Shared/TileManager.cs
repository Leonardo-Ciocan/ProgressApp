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

        public async static void SaveAndPin(UIElement tile , UIElement smallTile, String ID)
        {
            SecondaryTile pinTile = null;

            if (smallTile != null)
            {
                RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap();
                await renderTargetBitmap.RenderAsync(smallTile);
                var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();

                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                var tileFile = await storageFolder.CreateFileAsync(ID + "_medium.png", CreationCollisionOption.ReplaceExisting);

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
                var tile2 = new SecondaryTile(ID, "a", "b", new Uri("ms-appdata:///local/" + ID + "_medium.png"), TileSize.Default);
                tile2.VisualElements.Square150x150Logo = new Uri("ms-appdata:///local/" + ID + "_medium.png");
                pinTile = tile2;
            }


            if (tile != null)
            {
                RenderTargetBitmap renderTargetBitmap = new RenderTargetBitmap();
                await renderTargetBitmap.RenderAsync(tile);
                var pixelBuffer = await renderTargetBitmap.GetPixelsAsync();

                StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
                var tileFile = await storageFolder.CreateFileAsync(ID + "_wide.png", CreationCollisionOption.ReplaceExisting);

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
                //var tile2 = new SecondaryTile(ID, "a", "b", new Uri("ms-appdata:///local/" + ID + "_wide.png"), TileSize.Wide310x150);
                pinTile.VisualElements.Wide310x150Logo = new Uri("ms-appdata:///local/" + ID + "_wide.png");
            }

            await pinTile.RequestCreateAsync();

            
        }
    }
}
