using System;
using System.IO;
using AppXF.Data;
using AppXF.iOS;
using SQLite;


[assembly: Xamarin.Forms.Dependency(typeof(SQLite_IOS))]
namespace AppXF.iOS
{
    class SQLite_IOS : ISQLite
    {
        private const string _db = "Plates.db";
        private static string _pathToDatabase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), _db);

        public SQLiteConnection GetConnection()
        {
            File.Open(_pathToDatabase, FileMode.OpenOrCreate);
            return new SQLiteConnection(_pathToDatabase, SQLiteOpenFlags.ReadWrite);
        }
    }
}
