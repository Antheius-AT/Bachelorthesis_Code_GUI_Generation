using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public abstract class InterfaceElementType<TModelType> : ISpecificationElementVisitable<TModelType> where TModelType : class
    {
        public InterfaceElementType(string stringRepresentation, Type dotnetTypeRepresentation)
        {
            StringRepresentation = stringRepresentation;
            DotnetTypeRepresentation = dotnetTypeRepresentation;
        }

        public Type DotnetTypeRepresentation { get; }
        public string StringRepresentation { get; }

        public abstract RenderFragment Accept(ISpecificationElementVisitor<TModelType> visitor);
    }
}
