using Domain.Entities;
using Domain.ValueTypes;
using NUnit.Framework;

namespace Tests.Mappings
{
    [TestFixture]
    public class CompanyMappingFixture : BaseFixture
    {
        [Test]
        public void CompanyProperties()
        {
            object entityId;

            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    entityId = session.Save(new Company
                    {
                        Name = "Company Name",
                        Address = new Address
                        {
                            Line1 = "Address Line 1",
                            Line2 = "Address Line 2",
                            PostalCode = "3213"
                        }
                    });

                    transaction.Commit();
                }
            }

            using (var session = OpenSession())
            {
                var field = session.Get<Company>(entityId);

                Assert.NotNull(field);
                Assert.AreEqual("Company Name", field.Name);
                Assert.AreEqual("Address Line 1", field.Address.Line1);
                Assert.AreEqual("Address Line 2", field.Address.Line2);
                Assert.AreEqual("3213", field.Address.PostalCode);
            }
        }
    }
}
