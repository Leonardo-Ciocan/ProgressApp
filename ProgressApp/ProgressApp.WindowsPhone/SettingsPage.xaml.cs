using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Windows.System.Profile;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkID=390556

namespace ProgressApp
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingsPage : Page
    {
        public SettingsPage()
        {
            this.InitializeComponent();
        }

        /// <summary>
        /// Invoked when this page is about to be displayed in a Frame.
        /// </summary>
        /// <param name="e">Event data that describes how this page was reached.
        /// This parameter is typically used to configure the page.</param>
        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            
            
           
            preview.Width = Window.Current.Bounds.Width;
            preview.Height = Window.Current.Bounds.Height;
            for (int x = 0; x < 3; x++)
            {
                ProgressItemControl cont = new ProgressItemControl();
                cont.DataContext = Core.items[x];
                preview.Children.Add(cont);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            
            /*if (!LockScreen.IsProvidedByCurrentApplication)
            {
                // If you're not the provider, this call will prompt the user for permission.
                // Calling RequestAccessAsync from a background agent is not allowed.
                await LockScreenManager.RequestAccessAsync();
            }
 
    // Only do further work if the access is granted.
            if (LockScreenManager.IsProvidedByCurrentApplication)
            {
            }*/
        }
    }
}
