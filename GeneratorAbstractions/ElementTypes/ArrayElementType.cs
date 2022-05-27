using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class ArrayElementType<TModelType> : InterfaceElementType<TModelType> where TModelType : class
    {
        public ArrayElementType(InterfaceElementType<TModelType> vectorType) : base("array", typeof(Array))
        {
            VectorType = vectorType;
        }

        public InterfaceElementType<TModelType> VectorType { get; }

        public override RenderFragment Accept(ISpecificationElementVisitor<TModelType> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
