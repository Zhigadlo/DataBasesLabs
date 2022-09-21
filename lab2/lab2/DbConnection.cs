using System.Configuration;

namespace lab2
{
    public class DbConnection
    {
        public static DbConnection Instance { get => _instance; }
        private static DbConnection _instance;
        private string? _connectionString;
        private DbConnection()
        {
            _connectionString = ConfigurationManager.AppSettings["cafedb"];
        }

        static DbConnection()
        {
            _instance = new DbConnection();
        }

        public string? GetConnectionString() => _connectionString;
    }
}
