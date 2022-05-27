using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class BooleanElementType : InterfaceElementType
    {
        public BooleanElementType() : base("bool")
        {
        }

        public override RenderFragment Accept(ISpecificationElementVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
