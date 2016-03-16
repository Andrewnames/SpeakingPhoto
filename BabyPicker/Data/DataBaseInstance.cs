
using Xamarin.Forms;

namespace BabyPicker.Data
{
    public static class DataBaseInstance
    {

        private static DatabaseConnect Database;
        public static DatabaseConnect DatabaseInstance
        {
            get
            {
                return Database ?? (Database =   new DatabaseConnect());
            }
        }

    }
}
