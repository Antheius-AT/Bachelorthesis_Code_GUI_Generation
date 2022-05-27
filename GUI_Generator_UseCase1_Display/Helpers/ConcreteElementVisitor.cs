using GeneratorSharedComponents;
using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GUI_Generator_UseCase1_Display.Helpers
{
    public class ConcreteElementVisitor : ISpecificationElementVisitor
    {
        public RenderFragment Visit(StringElementType element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(FloatElementType element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(IntegerElementType element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(DerivativeElementType element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(ActionElementType element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(BooleanElementType element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(ArrayElementType element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(ConditionalElementType element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(ContainerElementType element)
        {
            throw new NotImplementedException();
        }
    }
}
