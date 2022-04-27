using Domain.Entities;
using NUnit.Framework;

namespace Tests.Mappings
{
    [TestFixture]
    public class FieldMappingFixture : BaseFixture
    {
        [Test]
        [System.Obsolete]
        public void FieldProperties()
        {
            object entityId;

            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var templateId = session.Save(new Template());
                    entityId = session.Save(new Field
                    {
                        Name = "FieldName",
                        Page = 1,
                        Width = 50,
                        Heigth = 60,
                        Template = session.Load<Template>(templateId)
                    });

                    transaction.Commit();
                }
            }

            using (var session = OpenSession())
            {
                var entity = session.Get<Field>(entityId);

                Assert.NotNull(entity);
                Assert.AreEqual("FieldName", entity.Name);
                Assert.AreEqual(1, entity.Page);
                Assert.AreEqual(50, entity.Width);
                Assert.AreEqual(60, entity.Heigth);
            }
        }
    }
}
