using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;

namespace Persistence
{
    public class PostgreSqlSessionHelper : ISessionHelper
    {
        private readonly ISessionFactory _sessionFactory;

        public PostgreSqlSessionHelper()
        {
            var configuration = new Configuration()
                .DataBaseIntegration(db =>
                {
                    db.Dialect<PostgreSQL82Dialect>();
                    db.Driver<NpgsqlDriver>();
                    db.ConnectionProvider<DriverConnectionProvider>();
                    db.ConnectionStringName = "Persistence";
                    db.LogSqlInConsole = true;
                    db.LogFormattedSql = true;
                })
                .SetNamingStrategy(ImprovedNamingStrategy.Instance)
                .AddAssembly("Persistence");

            var export = new SchemaExport(configuration);
            export.Drop(true, true);
            export.Create(true, true);

            _sessionFactory = configuration.BuildSessionFactory();
        }

        public ISession OpenSession()
        {
            return _sessionFactory.OpenSession();
        }
    }
}
