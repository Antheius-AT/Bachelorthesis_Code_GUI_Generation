using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class IntegerElementType : InterfaceElementType
    {
        public IntegerElementType() : base("int")
        {
        }

        public override RenderFragment Accept(ISpecificationElementVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
