
namespace GeneratorSharedComponents.Abstractions
{
    /// <summary>
    /// Represents a single element of an abstract interface specification.
    /// </summary>
    public class InterfaceSpecificationElement<TModelType> where TModelType : class
    {
        public InterfaceSpecificationElement(InterfaceElementType<TModelType> elementType)
        {
            ElementType = elementType;
        }

        public InterfaceElementType<TModelType> ElementType { get; }
    }
}
