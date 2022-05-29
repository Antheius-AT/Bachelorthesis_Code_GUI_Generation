using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class FloatElementType<TModelType> : InterfaceElementType<TModelType> where TModelType : class
    {
        public FloatElementType(string binding, string? label) : base("float", typeof(float), binding, label)
        {
        }

        public override RenderFragment Accept(ISpecificationElementVisitor<TModelType> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
