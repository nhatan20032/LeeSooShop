using ServiceStack.OrmLite;

namespace Bll.Extentions
{
    public class DbServices
    {
        private const string ConnectionString = "Server=127.0.0.1;Database=leesooshop;User ID=root;Password=123456;SslMode=none;";

        private static readonly Lazy<OrmLiteConnectionFactory> _lazyConnectionData = new(() =>
        {
            OrmLiteConfig.DialectProvider = MySqlDialect.Provider;
            return new OrmLiteConnectionFactory(ConnectionString, OrmLiteConfig.DialectProvider);
        });

        public DbServices()
        {
        }
        public OrmLiteConnectionFactory _connectionData => _lazyConnectionData.Value;
    }
}
