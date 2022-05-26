using System.Xml.Linq;
using GUI_Generator_UseCase1_Display.IndividualParts.ElementTypes;

namespace GUI_Generator_UseCase1_Display.IndividualParts
{
    /// <summary>
    /// Represents a single element of an abstract interface specification.
    /// </summary>
    public class InterfaceSpecificationElement
    {
        public InterfaceSpecificationElement(InterfaceElementType elementType)
        {
            ElementType = elementType;
        }

        public InterfaceElementType ElementType { get; }
    }
}
