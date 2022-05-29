using GeneratorSharedComponents.Abstractions;
using GeneratorSharedComponents.MarkerTypes;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class ConditionalElementType<TModelType> : InterfaceElementType<TModelType> where TModelType : class
    {
        public ConditionalElementType(InterfaceElementType<TModelType> elementType, string constraintPropertyName, string? label) : base("conditional", typeof(Conditional), string.Empty, label)
        {
            ElementType = elementType;
            ConstraintPropertyName = constraintPropertyName;
        }

        public InterfaceElementType<TModelType> ElementType { get; }

        /// <summary>
        /// Rethink how to use this, wont stay a string.
        /// </summary>
        public string ConstraintPropertyName { get; }

        public override RenderFragment Accept(ISpecificationElementVisitor<TModelType> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
