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
using Newtonsoft.Json;
namespace ProgressApp
{
    public class Core
    {
        //public static bool loaded = false;
        public static ObservableCollection<ProgressItem> items = new ObservableCollection<ProgressItem>();

        public static StorageFolder storageFolder = ApplicationData.Current.RoamingFolder;

        public static void Initialize()
        {
            storageFolder = ApplicationData.Current.RoamingFolder;
        }

        public static async void LoadAllItems(){
            if (storageFolder != null)
            {
                var file = await storageFolder.CreateFileAsync("data.items", CreationCollisionOption.OpenIfExists);
                string data = await FileIO.ReadTextAsync(file);
                var result = JsonConvert.DeserializeObject<ObservableCollection<ProgressItem>>(data);
                if (result != null)
                {
                    foreach (ProgressItem item in result)
                    {
                        items.Add(item);
                    }
                }
            }
        }

        public static async void SaveAllItems()
        {
            var file = await storageFolder.CreateFileAsync("data.items" , CreationCollisionOption.OpenIfExists);
            string json = JsonConvert.SerializeObject(items);
            await FileIO.WriteTextAsync(file, json);
        }
    }
}
