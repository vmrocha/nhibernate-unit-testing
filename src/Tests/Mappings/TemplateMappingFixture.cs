using Domain.Entities;
using NUnit.Framework;
using System.Linq;

namespace Tests.Mappings
{
    [TestFixture]
    public class TemplateMappingFixture : BaseFixture
    {
        [Test]
        public void TemplateProperties()
        {
            object entityId = null;

            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    entityId = session.Save(new Template
                    {
                        Name = "Template Name",
                        Pages = 5,
                        Width = 500,
                        Heigth = 600
                    });

                    transaction.Commit();
                }
            }

            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var entity = session.Get<Template>(entityId);

                    Assert.NotNull(entity);
                    Assert.AreEqual("Template Name", entity.Name);
                    Assert.AreEqual(5, entity.Pages);
                    Assert.AreEqual(500, entity.Width);
                    Assert.AreEqual(600, entity.Heigth);
                }
            }
        }

        [Test]
        public void TemplateCompanyProperty()
        {
            object entityId = null;

            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    entityId = session.Save(new Template
                    {
                        Company = session.Load<Company>(
                            session.Save(new Company("Company Name")))
                    });

                    transaction.Commit();
                }
            }

            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var entity = session.Get<Template>(entityId);

                    Assert.NotNull(entity);
                    Assert.AreEqual("Company Name", entity.Company.Name);
                }
            }
        }

        [Test]
        public void TemplateFieldsProperty()
        {
            object entityId = null;

            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var template = new Template();
                    template.AddField(new Field("FieldOne"));
                    template.AddField(new Field("FieldTwo"));

                    entityId = session.Save(template);

                    transaction.Commit();
                }
            }

            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var entity = session.Get<Template>(entityId);

                    Assert.NotNull(entity);
                    Assert.NotNull(entity.Fields);
                    Assert.AreEqual(2, entity.Fields.Count);
                    Assert.Contains("FieldOne", entity.Fields.Select(x => x.Name).ToList());
                    Assert.Contains("FieldTwo", entity.Fields.Select(x => x.Name).ToList());
                }
            }
        }
    }
}
