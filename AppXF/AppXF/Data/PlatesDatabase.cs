using System.Collections.Generic;
using System.Linq;
using AppXF.Model;
using SQLite;
using Xamarin.Forms;

namespace AppXF.Data
{
    public class PlatesDatabase
    {
        private SQLiteConnection database;
        private static object Locker = new object();


        public PlatesDatabase()
        {
            database = DependencyService.Get<ISQLite>().GetConnection();
            database.CreateTable<DataPlate>();
        }


        public int SavePlate(DataPlate dataPlate)
        {
            lock (Locker)
            {
                if (dataPlate.ID != 0)
                {
                    database.Update(dataPlate);
                    return dataPlate.ID;
                }
                return database.Insert(dataPlate);
            }
        }


        public IEnumerable<DataPlate> GetDataPlates(string category)
        {
            lock (Locker)
            {
                return database.Table<DataPlate>().Where(x => x.Category == category).ToList();
            }
        }

        public DataPlate GetDataPlate(int id)
        {
            lock (Locker)
            {
                return database.Table<DataPlate>().FirstOrDefault(c => c.ID == id);
            }
        }

        public int DeletPlate(int id)
        {
            lock (Locker)
            {
                return database.Delete<DataPlate>(id);
            }
        }
    }
}
