using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Metadata
{
    public class ValueKindAttribute : Attribute
    {
        public ValueKindAttribute(Type valueKind)
        {
            ValueKind = valueKind;
        }

        public Type ValueKind { get; }
    }
}
