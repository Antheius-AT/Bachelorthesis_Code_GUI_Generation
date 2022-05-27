using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GeneratorSharedComponents.Abstractions
{
    public interface IXMLSpecificationConverter
    {
        IEnumerable<InterfaceSpecificationElement> TransformToElementCollection(XElement root);
    }
}
