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

    [System.Obsolete]
    protected ISession OpenSession()
        {
            return _sessionHelper.OpenSession();
        }

        [OneTimeTearDown]
        public void TearDown()
        {
            _sessionHelper.Dispose();
        }
    }
}
