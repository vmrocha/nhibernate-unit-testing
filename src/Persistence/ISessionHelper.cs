using NHibernate;

namespace Persistence
{
    public interface ISessionHelper
    {
        ISession OpenSession();
    }
}
