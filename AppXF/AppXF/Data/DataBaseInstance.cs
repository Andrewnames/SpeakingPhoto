namespace AppXF.Data
{
    public static class DataBaseInstance
    {
        private static PlatesDatabase Database;
        public static PlatesDatabase DatabaseInstance => Database ?? (Database = new PlatesDatabase());
    }
}
