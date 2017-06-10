using System;

namespace Domain.Entities
{
    public abstract class BaseEntity
    {
        public virtual Guid Id { get; set; }
    }
}
