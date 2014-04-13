using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using Windows.UI;

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

        public double Minimum { get; set; }
        public double Maximum { get; set; }

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
            }
        }


        public string Unit { get; set; }

        public List<string> Tags { get; set; }

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

        public ProgressItem()
        {
            if (ID == null) ID = Guid.NewGuid().ToString();
        }


        public event PropertyChangedEventHandler PropertyChanged;
        private void RaisePropertyChanged( [CallerMemberName] string caller = "")
        {
            if ( PropertyChanged != null )
            {
                PropertyChanged( this, new PropertyChangedEventArgs( caller ) );
            }
        }
    }
}
