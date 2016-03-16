using SQLite;

namespace AppXF.Data
{
    public interface ISQLite
    {
       SQLiteConnection GetConnection();
    }
}
