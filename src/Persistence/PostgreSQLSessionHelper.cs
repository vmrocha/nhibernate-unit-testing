using NHibernate;
using NHibernate.Cfg;
using NHibernate.Connection;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;

namespace Persistence
{
    public class PostgreSQLSessionHelper
    {
        private static readonly ISessionFactory sessionFactory;

        static PostgreSQLSessionHelper()
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

            sessionFactory = configuration.BuildSessionFactory();
        }

        public static ISession OpenSession()
        {
            return sessionFactory.OpenSession();
        }
    }
}
