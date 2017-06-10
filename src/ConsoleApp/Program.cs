using Domain.Entities;
using Persistence;
using System;
using System.Linq;

namespace ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            object templateId = null;

            using (var session = PostgreSQLSessionHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var companyId = session.Save(new Company("Company"));

                    var template = new Template();
                    template.Name = "Template";
                    template.Company = session.Load<Company>(companyId);
                    template.AddField(new Field("FieldOne"));
                    template.AddField(new Field("FieldTwo"));

                    templateId = session.Save(template);

                    transaction.Commit();
                }
            }

            using (var session = PostgreSQLSessionHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
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
}
