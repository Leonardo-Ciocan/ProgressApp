using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using System.Xml.Serialization;
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
    public class Core
    {
        public static ObservableCollection<ProgressItem> items;
        public static XmlSerializer serializer = new XmlSerializer(typeof(ObservableCollection<ProgressItem>));
        public static StorageFolder storageFolder = ApplicationData.Current.RoamingFolder;

        public static async void LoadAllItems(){
            var file = await storageFolder.CreateFileAsync("data.items", CreationCollisionOption.OpenIfExists);
            using (Stream str = await file.OpenStreamForReadAsync())
            {
                items = serializer.Deserialize(str) as ObservableCollection<ProgressItem>;
            }
            if (items == null) items = new ObservableCollection<ProgressItem>();
        }

        public static async void SaveAllItems()
        {
            var file = await storageFolder.CreateFileAsync("data.items" , CreationCollisionOption.OpenIfExists);
            using (Stream str = await file.OpenStreamForWriteAsync())
            {
                serializer.Serialize(str, items);
            }
        }
    }
}
