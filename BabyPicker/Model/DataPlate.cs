using System;
using System.Collections.Generic;
using System.Text;
using SQLite;
using CoreData;
using SQLitePCL;

namespace BabyPicker.Model
{
    public class DataPlate
    {

        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public string ItemName { get; set; }
        public string ItemImage { get; set; }
        public string ItemAudio { get; set; }

    }

}
