using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public abstract class InterfaceElementType<TModelType> : ISpecificationElementVisitable<TModelType> where TModelType : class
    {
        public InterfaceElementType(string stringRepresentation)
        {
            StringRepresentation = stringRepresentation;
        }

        public string StringRepresentation { get; }

        public abstract RenderFragment Accept(ISpecificationElementVisitor<TModelType> visitor);
    }
}
