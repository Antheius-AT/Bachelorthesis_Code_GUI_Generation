using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class ArrayElementType : InterfaceElementType
    {
        public ArrayElementType(InterfaceElementType vectorType) : base("array")
        {
            VectorType = vectorType;
        }

        public InterfaceElementType VectorType { get; }

        public override RenderFragment Accept(ISpecificationElementVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
