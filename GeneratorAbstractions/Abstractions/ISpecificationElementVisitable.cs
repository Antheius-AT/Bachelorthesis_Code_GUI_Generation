
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents.Abstractions
{
    public interface ISpecificationElementVisitable
    {
        RenderFragment Accept(ISpecificationElementVisitor visitor);
    }
}
