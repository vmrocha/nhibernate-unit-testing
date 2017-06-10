using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.ValueTypes
{
    public class Address
    {
        public virtual string Line1 { get; set; }

        public virtual string Line2 { get; set; }

        public virtual string PostalCode { get; set; }
    }
}
