using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Metadata.Wellknown
{
    /// <summary>
    /// Represent an attribute that may be inherited from for use cases
    /// that are deemed so prevalent that they are well known and certain
    /// style guidelines are pre determined for them.
    /// </summary>
    public abstract class WellKnownAttribute : Attribute
    {
    }
}
