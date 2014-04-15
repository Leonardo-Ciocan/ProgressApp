using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace ProgressApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        bool loaded;
        public MainPage()
        {
            this.InitializeComponent();

            this.NavigationCacheMode = NavigationCacheMode.Required;
            StatusBar.GetForCurrentView().ForegroundColor = Colors.Gray;

            Core.Initialize();
            Core.LoadAllItems();
            
        }

        //ObservableCollection<ProgressItem> items = new ObservableCollection<ProgressItem>();
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            StatusBar.GetForCurrentView().BackgroundColor = Colors.White;
            StatusBar.GetForCurrentView().BackgroundOpacity = 0;
            StatusBar.GetForCurrentView().ForegroundColor = Colors.Gray;
            list.DataContext = Core.items;
            if (!loaded)
            {
                
                //Core.items.Add(new ProgressItem { Name = "Budget", Minimum = 0, Maximum = 1800, Value = 350, Color = Colors.Green });
                //Core.items.Add(new ProgressItem { Name = "Pushups", Minimum = 0, Maximum = 20, Value = 16, Color = Colors.Red });
                //Core.items.Add(new ProgressItem { Name = "Free bus days", Minimum = 0, Maximum = 12, Value = 6, Color = Colors.Orange });
                //Core.items.Add(new ProgressItem { Name = "Protein", Minimum = 0, Maximum = 24, Value = 2, Color = Colors.Violet });
                

                list.OnSelected += (item) =>
                {
                    Frame.Navigate(typeof(DetailsPage), item);
                };

                loaded = true;
            }
        }

        private void AppBarToggleButton_Click(object sender, RoutedEventArgs e)
        {
            //reorder on
            list.setReorderMode(((AppBarToggleButton)sender).IsChecked.Value);
        }

        Random random = new Random();
        private void AppBarButton_Click(object sender, RoutedEventArgs e)
        {
            //adding
            ProgressItem item = new ProgressItem()
            {
                Minimum = 0,
                Maximum = 100,
                Value = 35,
                Color = Color.FromArgb(255, (byte)random.Next(255),
                    (byte)random.Next(255),
                    (byte)random.Next(255)),
                ID = Guid.NewGuid().ToString()
            };
            Core.items.Add(item);
            Frame.Navigate(typeof(DetailsPage), item);
        }
    }
}
