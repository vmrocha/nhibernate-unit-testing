using Domain.ValueTypes;

namespace Domain.Entities
{
    public class Company : BaseEntity
    {
        public Company()
        { }

        public Company(string name)
        {
            Name = name;
        }

        public virtual string Name { get; set; }

        public virtual Address Address { get; set; }
    }
}
