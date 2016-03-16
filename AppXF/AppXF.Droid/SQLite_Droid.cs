using System;
using System.IO;
using AppXF.Data;
using AppXF.Droid;
using SQLite;
using Xamarin.Forms;

[assembly: Dependency(typeof(SQLite_Droid))]
namespace AppXF.Droid
{
    class SQLite_Droid: ISQLite
    {
        public SQLiteConnection GetConnection()
        {
            var sqliteFilename = "Plates.db";
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, sqliteFilename);
            // Create the connection
            File.Open(path, FileMode.OpenOrCreate);
            var conn = new SQLiteConnection(path, SQLiteOpenFlags.ReadWrite);
            // Return the database connection
            return conn;
        }
    }
}