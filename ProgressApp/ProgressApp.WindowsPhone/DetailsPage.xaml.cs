using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.StartScreen;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            DataContext = e.Parameter;
            StatusBar.GetForCurrentView().BackgroundColor = (e.Parameter as ProgressItem).Color;
            StatusBar.GetForCurrentView().BackgroundOpacity = 1;
            StatusBar.GetForCurrentView().ForegroundColor = Colors.White;

            var uriLogo = new Uri("ms-appx:///Assets/Logo.scale-240.png");
            //var uriSmallLogo = new Uri("ms-appx:///images/smallLogoSecondaryTile-sdk.png");



            //var tile = new SecondaryTile("n3wtile42", "a", "b", new Uri("ms-appx:///Assets/WideLogo.scale-240.png"), TileSize.Default);
            //ShellTileData data;
            //await tile.RequestCreateAsync();



        }
    }
}
