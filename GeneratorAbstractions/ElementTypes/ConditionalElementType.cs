using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class ConditionalElementType : InterfaceElementType
    {
        public ConditionalElementType(InterfaceElementType elementType, string constraintSatisfied) : base("conditional")
        {
            ElementType = elementType;
            ConstraintSatisfied = constraintSatisfied;
        }

        public InterfaceElementType ElementType { get; }

        /// <summary>
        /// Rethink how to use this, wont stay a string.
        /// </summary>
        public string ConstraintSatisfied { get; }

        public override RenderFragment Accept(ISpecificationElementVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
