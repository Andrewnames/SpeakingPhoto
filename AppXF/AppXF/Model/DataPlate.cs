using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;
using SQLite;
using SQLitePCL;
using Xamarin.Forms;

namespace AppXF.Model
{
    public class DataPlate: INotifyPropertyChanged
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }
        public string ItemName { get; set; }
        public string ImagePath { get; set; }

        public string AudioPath { get; set; }
        public bool IsSynth { get; set; }
        public string Category { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }

}
