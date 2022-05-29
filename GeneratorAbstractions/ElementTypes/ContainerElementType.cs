using GeneratorSharedComponents.Abstractions;
using GeneratorSharedComponents.MarkerTypes;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class ContainerElementType<TModelType> : InterfaceElementType<TModelType> where TModelType : class
    {
        public ContainerElementType(IEnumerable<InterfaceSpecificationElement<TModelType>> contentElements, string binding, string? label) : base("container", typeof(Container), binding, label)
        {
            ContentElements = contentElements;
        }

        public IEnumerable<InterfaceSpecificationElement<TModelType>> ContentElements { get; }

        public override RenderFragment Accept(ISpecificationElementVisitor<TModelType> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
