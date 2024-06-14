using ServiceStack.OrmLite;

namespace Bll.Extentions
{
    public class DbServices
    {
        private const string ConnectionString = "Server=localhost;Database=leesooshop;User ID=root;Password=123456;SslMode=none;AllowPublicKeyRetrieval=True;";

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
