namespace Domain.Entities
{
    public class Field : BaseEntity
    {
        public Field()
        { }

        public Field(string name)
        {
            // ReSharper disable once VirtualMemberCallInConstructor
            Name = name;
        }

        public virtual string Name { get; set; }

        public virtual int Page { get; set; }

        public virtual float Width { get; set; }

        public virtual float Heigth { get; set; }

        public virtual Template Template { get; set; }
    }
}
