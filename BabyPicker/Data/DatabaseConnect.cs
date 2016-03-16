using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using BabyPicker.Model;
using Foundation;
using SQLite;

namespace BabyPicker.Data
{
    public class DatabaseConnect 
    {
       // private SQLiteConnection Database;
        static object Locker = new object();
        private const string _db = "Plates.db";
//private static string _pathToDatabase = Path.Combine(Environment.CurrentDirectory, _db);
         private static string _pathToDatabase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), _db );
        
        public static SQLiteConnection GetConnection()        {

            //  var conn = new SQLiteConnection(_pathToDatabase);
            File.Open(_pathToDatabase, FileMode.OpenOrCreate);
            var conn = new SQLiteConnection(_pathToDatabase, SQLiteOpenFlags.ReadWrite);

           
            // Return the Database connection 
            return conn;
        }

        public DatabaseConnect()
        {
            //using (var conn = GetConnection())
            //{
                
            //    conn.CreateTable<DataPlate>();
                

            //}
        }

        public int SavePlate(DataPlate dataPlate)
        {
            lock (Locker)
            {
                using (var conn = GetConnection())
                {
                    if (dataPlate.ID != 0)
                    {
                        conn.Update(dataPlate);
                        return dataPlate.ID;
                    }

                    return conn.Insert(dataPlate); 
                    
                }
            }
        }


        public IEnumerable<DataPlate> GetDataPlates()
        {
            lock (Locker)
            {
                using (var conn = GetConnection())
                {
                    return (from c in conn.Table<DataPlate>()
                            select c).ToList(); 
                }
            }
        }

       

        public DataPlate GetDataPlate(int id)
        {
            lock (Locker)
            {
                using (var conn = GetConnection())
                {
                    return conn.Table<DataPlate>().FirstOrDefault(c => c.ID == id);
                }
            }
        }

        public int DeletPlate(int id)
        {
            lock (Locker)
            {
                using (var conn = GetConnection())
                {
                    return conn.Delete<DataPlate>(id);
                }
            }
        }

         
    }


}
