using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class ContainerElementType : InterfaceElementType
    {
        public ContainerElementType(IEnumerable<InterfaceElementType> contentElements) : base("container")
        {
            ContentElements = contentElements;
        }

        public IEnumerable<InterfaceElementType> ContentElements { get; }

        public override RenderFragment Accept(ISpecificationElementVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
