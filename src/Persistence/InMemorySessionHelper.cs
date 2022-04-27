using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using System.Data.SQLite;

namespace Persistence
{
    public class InMemorySessionHelper : ISessionHelper, IDisposable
    {
        private const string Connectionstring = "Data Source=:memory:";

        private readonly Configuration _config;
        private readonly ISessionFactory _sessionFactory;
        private SQLiteConnection _connection;

        public InMemorySessionHelper()
        {
            _config = new Configuration()
                .DataBaseIntegration(db =>
                {
                    db.Dialect<SQLiteDialect>();
                    db.Driver<SQLite20Driver>();
                    db.ConnectionString = Connectionstring;
                    db.LogFormattedSql = true;
                    db.LogSqlInConsole = true;
                })
                .AddAssembly("Persistence");

            _sessionFactory = _config.BuildSessionFactory();
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

        [Obsolete]
        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession(GetConnection());
        }

        private SQLiteConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SQLiteConnection(Connectionstring);
                _connection.Open();

                SchemaExport se = new SchemaExport(_config);
                se.Execute(true, true, false, _connection, null);
            }

            return _connection;
        }
    }
}
