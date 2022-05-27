using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace GeneratorSharedComponents.Abstractions
{
    public interface IXMLSpecificationConverter<TModelType> where TModelType : class
    {
        IEnumerable<InterfaceSpecificationElement<TModelType>> TransformToElementCollection(XElement root);
    }
}
