using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;
using System.Data.SQLite;
using System.Reflection;

namespace Persistence
{
    public class InMemorySessionHelper
    {
        private const string CONNECTION_STRING = "Data Source=:memory:;Version=3;New=True;";

        private Configuration _config;
        private ISessionFactory _sessionFactory;
        private SQLiteConnection _connection;

        public InMemorySessionHelper()
        {
            _config = new Configuration()
                .DataBaseIntegration(db =>
                {
                    db.Dialect<SQLiteDialect>();
                    db.Driver<SQLite20Driver>();
                    db.ConnectionString = CONNECTION_STRING;
                    db.LogSqlInConsole = true;
                    db.LogFormattedSql = true;
                })
                .SetNamingStrategy(ImprovedNamingStrategy.Instance)
                .SetProperty(Environment.CurrentSessionContextClass, "thread_static")
                .AddAssembly("Persistence");

            _sessionFactory = _config.BuildSessionFactory();
        }

        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession(GetConnection());
        }

        private SQLiteConnection GetConnection()
        {
            if (_connection == null)
            {
                _connection = new SQLiteConnection(CONNECTION_STRING);
                _connection.Open();

                SchemaExport se = new SchemaExport(_config);
                se.Execute(true, true, false, _connection, null);
            }

            return _connection;
        }
    }
}
