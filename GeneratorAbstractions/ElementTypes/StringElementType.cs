using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class StringElementType : InterfaceElementType
    {
        public StringElementType() : base("string")
        {
        }

        public override RenderFragment Accept(ISpecificationElementVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
