using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BabyPicker.CellModel;
using BabyPicker.Data;
using BabyPicker.Model;
using SQLite;

namespace BabyPicker.Services
{
    public class PlateDataServices
    {
        private DatabaseConnect db;
        public PlateDataServices()
        {
            db = DataBaseInstance.DatabaseInstance;
        }
        public List<DataPlate> GetPlateCells()
        {
            return db.GetDataPlates().ToList();
        }
 

        public DataPlate GetPlateCell(int id)
        {
            return db.GetDataPlate(id);
        }

        public void SaveDataPlate(DataPlate dataPlate)
        {
            db.SavePlate(dataPlate);
        }



    }
}
