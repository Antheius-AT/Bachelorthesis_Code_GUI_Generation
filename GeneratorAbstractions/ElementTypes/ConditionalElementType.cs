using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class ConditionalElementType<TModelType> : InterfaceElementType<TModelType> where TModelType : class
    {
        public ConditionalElementType(InterfaceElementType<TModelType> elementType, bool constraintSatisfied) : base("conditional")
        {
            ElementType = elementType;
            ConstraintSatisfied = constraintSatisfied;
        }

        public InterfaceElementType<TModelType> ElementType { get; }

        /// <summary>
        /// Rethink how to use this, wont stay a string.
        /// </summary>
        public bool ConstraintSatisfied { get; }

        public override RenderFragment Accept(ISpecificationElementVisitor<TModelType> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
