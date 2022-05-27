using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class BooleanElementType<TModelType> : InterfaceElementType<TModelType> where TModelType : class
    {
        public BooleanElementType() : base("bool")
        {
        }

        public override RenderFragment Accept(ISpecificationElementVisitor<TModelType> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
