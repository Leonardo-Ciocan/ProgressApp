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
using Windows.UI.Popups;
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
            //StatusBar.GetForCurrentView().BackgroundColor = Color.FromArgb(0, 0, 0, 1);

            StatusBar.GetForCurrentView().BackgroundColor = (e.Parameter as ProgressItem).Color;
            //this.BottomAppBar.Background = new SolidColorBrush((e.Parameter as ProgressItem).Color);
            this.BottomAppBar.Foreground = new SolidColorBrush(Colors.White);
            StatusBar.GetForCurrentView().BackgroundOpacity = 1;
            StatusBar.GetForCurrentView().ForegroundColor = Colors.White;
            
            var uriLogo = new Uri("ms-appx:///Assets/Logo.scale-240.png");
            //var uriSmallLogo = new Uri("ms-appx:///images/smallLogoSecondaryTile-sdk.png");

            Core.SaveAllItems();
        }

        protected override void OnNavigatingFrom(NavigatingCancelEventArgs e)
        {
            Core.SaveAllItems();
            base.OnNavigatingFrom(e);
        }

        async void PinTile()
        {
            var item = tileSlider;
            TileManager.SaveAndPin(tile,tileSmall, self.ID);
        }

        private void PinClicked(object sender, RoutedEventArgs e)
        {
            PinTile();
        }

        private async  void DeleteClicked(object sender, RoutedEventArgs e)
        {
            var dialog = new MessageDialog("Are you sure?" , "Deleting " + self.Name);
            dialog.Commands.Add(new UICommand("Yes", (a) => {
                Core.items.Remove(self);
                Frame.Navigate(typeof(MainPage));
            }));
            dialog.Commands.Add(new UICommand("Cancel"));
            await dialog.ShowAsync();

        }
    }
}
