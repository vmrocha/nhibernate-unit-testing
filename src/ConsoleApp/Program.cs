using Domain.Entities;
using Persistence;
using System;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        private static ISessionHelper _sessionHelper;

        static void Main(string[] args)
        {
            _sessionHelper = new PostgreSqlSessionHelper();

            object templateId;

            using (var session = _sessionHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var companyId = session.Save(new Company("Company"));

                    var template = new Template
                    {
                        Name = "Template",
                        Company = session.Load<Company>(companyId)
                    };
                    template.AddField(new Field("FieldOne"));
                    template.AddField(new Field("FieldTwo"));

                    templateId = session.Save(template);

                    transaction.Commit();
                }
            }

            using (var session = _sessionHelper.OpenSession())
            {
                var template = session
                    .QueryOver<Template>()
                    .Fetch(x => x.Company).Eager
                    .Fetch(x => x.Fields).Eager
                    .Where(x => x.Id == (Guid)templateId)
                    .SingleOrDefault();

                Console.WriteLine($"Name: {template.Name}");
                Console.WriteLine($"Company: {template.Company.Name}");
                Console.WriteLine($"Fields: {string.Join(", ", template.Fields.Select(f => f.Name))}");
            }
        }
    }
}
