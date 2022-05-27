using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public abstract class InterfaceElementType : ISpecificationElementVisitable
    {
        public InterfaceElementType(string stringRepresentation)
        {
            StringRepresentation = stringRepresentation;
        }

        public string StringRepresentation { get; }

        public abstract RenderFragment Accept(ISpecificationElementVisitor visitor);
    }
}
