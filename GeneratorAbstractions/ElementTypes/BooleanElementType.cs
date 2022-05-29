using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class BooleanElementType<TModelType> : InterfaceElementType<TModelType> where TModelType : class
    {
        public BooleanElementType(string binding, string? label) : base("bool", typeof(bool), binding, label)
        {
        }

        public override RenderFragment Accept(ISpecificationElementVisitor<TModelType> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
