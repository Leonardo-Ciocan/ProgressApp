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
    public sealed partial class ProgressListControl : UserControl
    {
        public delegate void SelectedHandler(ProgressItem item);
        public event SelectedHandler OnSelected;

        public ProgressListControl()
        {
            this.InitializeComponent();
            DataContextChanged += ProgressListControl_DataContextChanged;
            Loaded += ProgressListControl_Loaded;

            root.SelectionChanged += (a, b) =>
            {
                if (root.SelectedIndex != -1)
                {
                    OnSelected(root.SelectedValue as ProgressItem);
                    root.SelectedIndex = -1;
                }
            };
        }

        void ProgressListControl_Loaded(object sender, RoutedEventArgs e)
        {

        }

        void ProgressListControl_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            root.ItemsSource = DataContext;
        }

        public void setReorderMode(bool reorder)
        {
#if WINDOWS_PHONE_APP
            root.ReorderMode = reorder ? Windows.UI.Xaml.Controls.ListViewReorderMode.Enabled : Windows.UI.Xaml.Controls.ListViewReorderMode.Disabled;
#endif
        }
    }
}
