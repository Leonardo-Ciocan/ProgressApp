using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
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
    public sealed partial class ColorPicker : UserControl, INotifyPropertyChanged
    {
        private Color _color;
        public Color PickedColor
        {
            get
            {
                return _color;
            }
            set
            {
                _color = value;
                RaisePropertyChanged();
            }

        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged([CallerMemberName] string caller = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(caller));
            }
        }

        public ColorPicker()
        {
            this.InitializeComponent();
            this.Loaded += ColorPicker_Loaded;
        }

        Random random = new Random();
        void ColorPicker_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Color> colors = new ObservableCollection<Color>();

            for (int x = 0; x < 25; x++)
            {
                colors.Add(Color.FromArgb(255, (byte)random.Next(255),
                    (byte)random.Next(255),
                    (byte)random.Next(255)));
            }



            root.ItemsSource = colors;

            root.SelectionChanged += (a, b) =>
            {
                (DataContext as ProgressItem).Color = (Color) root.SelectedValue;
            };

        }
    }
}
