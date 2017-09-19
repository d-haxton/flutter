namespace Flutter.Settings
{
    public class DatabaseSettings
    {
        public string DatabaseName { get; }

        public DatabaseSettings(string databaseName)
        {
            DatabaseName = databaseName;
        }
    }
}
