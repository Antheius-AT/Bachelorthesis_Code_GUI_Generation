using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class ContainerElementType<TModelType> : InterfaceElementType<TModelType> where TModelType : class
    {
        public ContainerElementType(IEnumerable<InterfaceElementType<TModelType>> contentElements) : base("container")
        {
            ContentElements = contentElements;
        }

        public IEnumerable<InterfaceElementType<TModelType>> ContentElements { get; }

        public override RenderFragment Accept(ISpecificationElementVisitor<TModelType> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
