using System.Xml.Linq;
using GeneratorSharedComponents;
using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;
using Models.UseCases.DisplayOnly.UseCase2;
using Radzen.Blazor;

namespace GUI_Generator_UseCase1_Display.Helpers
{
    public class DefaultElementVisitor : ISpecificationElementVisitor<PersonalDetails>
    {
        private PersonalDetails? concreteData;
        private DeviceModel<PersonalDetails>? deviceModel;

        public void SetData(PersonalDetails data)
        {
            concreteData = data;
        }

        public void SetDeviceModel(DeviceModel<PersonalDetails> deviceModel)
        {
            this.deviceModel = deviceModel;
        }

        public RenderFragment Visit(StringElementType<PersonalDetails> element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(FloatElementType<PersonalDetails> element)
        {
            if (concreteData == null)
            {
                throw new InvalidOperationException("Setting the concrete data is required before attempting to generate a render fragment");
            }

            if (deviceModel == null)
            {
                throw new InvalidOperationException("Setting the device model is required before attempting to generate a render fragment");
            }

            var property = concreteData.GetType().GetProperties().SingleOrDefault(p => p.Name == element.Binding) ?? throw new InvalidOperationException($"Specified instance did not contain property associated with the specified binding {element.Binding}");
            float value = Convert.ToSingle(property.GetValue(concreteData));

            return BuildRenderTree(value, element);
        }

        public RenderFragment Visit(integerelementType<PersonalDetails> element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(DerivativeElementType<PersonalDetails> element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(ActionElementType<PersonalDetails> element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(BooleanElementType<PersonalDetails> element)
        {
            if (concreteData == null)
            {
                throw new InvalidOperationException("Setting the concrete data is required before attempting to generate a render fragment");
            }

            if (deviceModel == null)
            {
                throw new InvalidOperationException("Setting the device model is required before attempting to generate a render fragment");
            }

            var property = concreteData.GetType().GetProperties().SingleOrDefault(p => p.Name == element.Binding) ?? throw new InvalidOperationException($"Specified instance did not contain property associated with the specified binding {element.Binding}");
            bool value = Convert.ToBoolean(property.GetValue(concreteData));

            return BuildRenderTree(value, element);
        }

        public RenderFragment Visit(ArrayElementType<PersonalDetails> element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(ConditionalElementType<PersonalDetails> element)
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

        public RenderFragment Visit(ContainerElementType<PersonalDetails> element)
        {
            if (concreteData == null)
            {
                throw new InvalidOperationException("Setting the concrete data is required before attempting to generate a render fragment");
            }

            if (deviceModel == null)
            {
                throw new InvalidOperationException("Setting the device model is required before attempting to generate a render fragment");
            }

            var containerProperty = concreteData.GetType().GetProperties().SingleOrDefault(p => p.Name == element.Binding) ?? throw new InvalidOperationException($"Specified instance did not contain property associated with the specified binding {element.Binding}");
            var containerInstance = containerProperty.GetValue(concreteData);
            // Think about how to best do this.
            throw new Exception();
            //return new RenderFragment(builder =>
            //{
            //    builder.OpenComponent(1, )
            //    foreach (var item in element.ContentElements)
            //    {
            //        var elementProperty = concreteData.GetType().GetProperties().SingleOrDefault(p => p.Name == item.ElementType.Binding) ?? throw new InvalidOperationException($"Specified container type did not contain property associated with the specified binding. Path: {element.Binding}/{item.ElementType.Binding}");
            //        var elementValue = elementProperty.GetValue(containerInstance);
            //    }
            //});
        }

        private WidgetBase GetMostAppropriateWidget(InterfaceElementType<PersonalDetails> element)
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

        private RenderFragment BuildRenderTree(object value, InterfaceElementType<PersonalDetails> element)
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
