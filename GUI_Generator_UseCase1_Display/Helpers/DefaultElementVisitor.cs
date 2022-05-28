using System.Xml.Linq;
using GeneratorSharedComponents;
using GeneratorSharedComponents.Abstractions;
using GUI_Generator_UseCase1_Display.Widgets;
using Microsoft.AspNetCore.Components;
using Models.UseCases.DisplayOnly.UseCase1;
using Radzen.Blazor;

namespace GUI_Generator_UseCase1_Display.Helpers
{
    public class DefaultElementVisitor : ISpecificationElementVisitor<SensorData>
    {
        private SensorData? concreteData;
        private DeviceModel<SensorData>? deviceModel;

        public void SetData(SensorData data)
        {
            concreteData = data;
        }

        public void SetDeviceModel(DeviceModel<SensorData> deviceModel)
        {
            this.deviceModel = deviceModel;
        }

        public RenderFragment Visit(StringElementType<SensorData> element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(FloatElementType<SensorData> element)
        {
            if (concreteData == null)
            {
                throw new InvalidOperationException("Setting the concrete data is required before attempting to generate a render fragment");
            }

            if (deviceModel == null)
            {
                throw new InvalidOperationException("Setting the device model is required before attempting to generate a render fragment");
            }

            var property = concreteData.GetType().GetProperties().SingleOrDefault(p => p.Name == element.BindingPath) ?? throw new InvalidOperationException($"Specified instance did not contain property associated with the specified binding {element.BindingPath}");
            float value = Convert.ToSingle(property.GetValue(concreteData));

            return BuildRenderTree(value, element);
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
            if (concreteData == null)
            {
                throw new InvalidOperationException("Setting the concrete data is required before attempting to generate a render fragment");
            }

            if (deviceModel == null)
            {
                throw new InvalidOperationException("Setting the device model is required before attempting to generate a render fragment");
            }

            var property = concreteData.GetType().GetProperties().SingleOrDefault(p => p.Name == element.BindingPath) ?? throw new InvalidOperationException($"Specified instance did not contain property associated with the specified binding {element.BindingPath}");
            bool value = Convert.ToBoolean(property.GetValue(concreteData));

            return BuildRenderTree(value, element);
        }

        public RenderFragment Visit(ArrayElementType<SensorData> element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(ConditionalElementType<SensorData> element)
        {
            if (concreteData == null)
            {
                throw new InvalidOperationException("Setting the concrete data is required before attempting to generate a render fragment");
            }

            if (deviceModel == null)
            {
                throw new InvalidOperationException("Setting the device model is required before attempting to generate a render fragment");
            }

            var constraintProperty = concreteData.GetType().GetProperties().SingleOrDefault(p => p.Name == element.ConstraintPropertyName) ?? throw new InvalidOperationException($"Specified instance did not contain property associated with the specified binding {element.ConstraintPropertyName}");
            bool value = Convert.ToBoolean(constraintProperty.GetValue(concreteData));

            if (value)
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

        private WidgetBase GetMostAppropriateWidget(InterfaceElementType<SensorData> element)
        {
            int currentHigh = -1;
            WidgetBase? currentWidget = null;

            foreach (var item in deviceModel!.Templates)
            {
                var elementScore = deviceModel.AppropriatenessMeasuringFunction.Invoke(element, item);

                if (currentHigh < elementScore)
                {
                    currentHigh = elementScore;
                    currentWidget = item;
                }
            }

            return currentWidget ?? throw new ArgumentException(nameof(element), $"No widget could be found for the specified element: {element}");
        }

        private RenderFragment BuildRenderTree(object value, InterfaceElementType<SensorData> element)
        {
            var widget = this.GetMostAppropriateWidget(element);

            return new RenderFragment(builder =>
            {
                builder.OpenComponent(1, widget.GetType());
                builder.AddAttribute(2, "Value", value);
                builder.AddAttribute(3, "Label", element.Label);
                builder.CloseComponent();
                builder.AddMarkupContent(3, "<br/>");
            });
        }
    }
}
