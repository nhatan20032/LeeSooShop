using ServiceStack.OrmLite;

namespace Bll.Extentions
{
    public class DbServices
    {
        private const string ConnectionString = "server=127.0.0.1;UID=root;PWD=123456!;database=leesooshop;Character Set=utf8;";

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
