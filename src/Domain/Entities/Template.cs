namespace Domain.Entities
{
    public class Template : BaseEntity
    {
        public Template()
        {
        }

        public Template(string name)
        {
            Name = name;
        }

        public virtual string Name { get; set; }

        public virtual int Pages { get; set; }

        public virtual float Width { get; set; }

        public virtual float Heigth { get; set; }

        public virtual Company Company { get; set; }

        public virtual ISet<Field> Fields { get; set; } = new HashSet<Field>();

        public virtual void AddField(Field field)
        {
            Fields.Add(field);
            field.Template = this;
        }
    }
}
