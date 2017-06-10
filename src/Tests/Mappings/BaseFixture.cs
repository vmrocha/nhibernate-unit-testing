using NHibernate;
using NUnit.Framework;
using Persistence;

namespace Tests.Mappings
{
    public class BaseFixture
    {
        private InMemorySessionHelper _sessionHelper;

        [OneTimeSetUp]
        public void Setup()
        {
            _sessionHelper = new InMemorySessionHelper();
        }

        protected ISession OpenSession()
        {
            return _sessionHelper.OpenSession();
        }
    }
}
