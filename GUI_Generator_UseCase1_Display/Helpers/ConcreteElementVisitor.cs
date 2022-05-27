using GeneratorSharedComponents;
using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;
using Models.UseCases.DisplayOnly.UseCase1;

namespace GUI_Generator_UseCase1_Display.Helpers
{
    public class ConcreteElementVisitor : ISpecificationElementVisitor<SensorData>
    {
        private SensorData? concreteData;

        public void SetData(SensorData data)
        {
            concreteData = data;
        }

        public RenderFragment Visit(StringElementType<SensorData> element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(FloatElementType<SensorData> element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(integerelementType<SensorData> element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(DerivativeElementType<SensorData> element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(ActionElementType<SensorData> element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(BooleanElementType<SensorData> element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(ArrayElementType<SensorData> element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(ConditionalElementType<SensorData> element)
        {
            if (element.ConstraintSatisfied)
            {
                return element.ElementType.Accept(this);
            }
            else
            {
                return new RenderFragment(_ => { });
            }
        }

        public RenderFragment Visit(ContainerElementType<SensorData> element)
        {
            throw new NotImplementedException();
        }
    }
}
