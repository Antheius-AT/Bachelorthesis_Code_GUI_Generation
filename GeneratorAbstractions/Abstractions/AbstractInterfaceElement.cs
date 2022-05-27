
namespace GeneratorSharedComponents.Abstractions
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
