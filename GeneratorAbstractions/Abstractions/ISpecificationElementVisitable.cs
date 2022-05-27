
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents.Abstractions
{
    public interface ISpecificationElementVisitable<TModelType> where TModelType : class
    {
        RenderFragment Accept(ISpecificationElementVisitor<TModelType> visitor);
    }
}
