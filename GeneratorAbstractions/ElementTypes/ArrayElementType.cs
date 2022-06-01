using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class ArrayElementType<TModelType> : InterfaceElementType<TModelType> where TModelType : class
    {
        public ArrayElementType(string binding, string? label) : base("array", typeof(Array), binding, label)
        {
        }

        public override RenderFragment Accept(ISpecificationElementVisitor<TModelType> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
