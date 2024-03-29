﻿using System;
using System.Collections.Generic;
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
    public class ProgressItem : INotifyPropertyChanged
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
                RaisePropertyChanged();
            }
        }

        private double _min;
        public double Minimum
        {
            get
            {
                return _min;
            }
            set
            {
                _min = value;
                RaisePropertyChanged();
            }
        }

        private double _max;
        public double Maximum
        {
            get
            {
                return _max;
            }
            set
            {
                _max = value;
                RaisePropertyChanged();
            }
        }

        private double _value;
        public double Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                RaisePropertyChanged();
                RaisePropertyChanged("LeftValue");
            }
        }

        [JsonIgnore]
        public double LeftValue
        {
            get
            {
                return Maximum - Value;
            }
        }


        private string _units;
        public string Units
        {
            get
            {
                return _units;
            }
            set
            {
                _units = value;
                RaisePropertyChanged();
            }
        }

        public string Tags { get; set; }

        private Color _color;
        public Color Color
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

        public string ID;



        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged( [CallerMemberName] string caller = "")
        {
            if ( PropertyChanged != null )
            {
                PropertyChanged( this, new PropertyChangedEventArgs( caller ) );
            }
        }


        /*
        StorageFolder storageFolder = ApplicationData.Current.LocalFolder;
        public async void Save()
        {
            var itemFile = await  storageFolder.CreateFileAsync(ID + ".item", CreationCollisionOption.OpenIfExists);
            using (var str = await itemFile.OpenStreamForWriteAsync())
            {
                serializer.Serialize(str, this);
            }
        }

        public async Task<ProgressItem> Load(StorageFile file)
        {
            ProgressItem item;
            using (var str = await file.OpenStreamForReadAsync())
            {
                item = serializer.Deserialize(str) as ProgressItem;
            }
            return item;
        }*/
    }
}
