using GeneratorSharedComponents;
using Microsoft.AspNetCore.Components;

namespace GUI_Generator_UseCase1_Display.Generator
{
    public interface ISpecificationElementVisitor
    {
        RenderFragment Visit(StringElementType element);
        RenderFragment Visit(FloatElementType element);
        RenderFragment Visit(IntegerElementType element);
        RenderFragment Visit(DerivativeElementType element);
        RenderFragment Visit(ActionElementType element);
        RenderFragment Visit(BooleanElementType element);
        RenderFragment Visit(ArrayElementType element);
        RenderFragment Visit(ConditionalElementType element);
        RenderFragment Visit(ContainerElementType element);

    }
}
