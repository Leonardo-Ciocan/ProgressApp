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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace ProgressApp
{
    public sealed partial class ProgressItemControl : UserControl
    {
        public bool UserEditable{
            get { return progressBar.IsEnabled; }
            set { progressBar.IsEnabled = value;
            progressBar.Visibility = (value) ? Visibility.Visible : Visibility.Collapsed;
            }
        }

        public ProgressItemControl()
        {
            this.InitializeComponent();
            DataContextChanged += ProgressItemControl_DataContextChanged;
            SizeChanged += ProgressItemControl_SizeChanged;
            progressBar.ValueChanged += (a, b) => updateBackground();
        }

        ProgressItem item;
        void ProgressItemControl_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            updateBackground();
        }

        void updateBackground()
        {
            try
            {
                progress.Width = (item.Value - item.Minimum) / (item.Maximum - item.Minimum) * ActualWidth;
                //left.Text = (item.Maximum - item.Value).ToString();
            }
            catch { }
        }

        void ProgressItemControl_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            item = DataContext as ProgressItem;
        }
    }
}
