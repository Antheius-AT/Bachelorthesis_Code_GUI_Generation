using System.Collections;
using System.Xml.Linq;
using GeneratorSharedComponents;
using GeneratorSharedComponents.Abstractions;
using GUI_Generator_UseCase2_Display.Widgets;
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
            EnsureReferences();

            var property = concreteData!.GetType().GetProperties().SingleOrDefault(p => p.Name == element.Binding) ?? throw new InvalidOperationException($"Specified instance did not contain property associated with the specified binding {element.Binding}");
            string value = property.GetValue(concreteData)!.ToString()!;

            return BuildRenderTree(value, element);
        }

        public RenderFragment Visit(FloatElementType<PersonalDetails> element)
        {
            EnsureReferences();

            var property = concreteData!.GetType().GetProperties().SingleOrDefault(p => p.Name == element.Binding) ?? throw new InvalidOperationException($"Specified instance did not contain property associated with the specified binding {element.Binding}");
            float value = Convert.ToSingle(property.GetValue(concreteData));

            return BuildRenderTree(value, element);
        }

        public RenderFragment Visit(integerelementType<PersonalDetails> element)
        {
            EnsureReferences();

            var property = concreteData!.GetType().GetProperties().SingleOrDefault(p => p.Name == element.Binding) ?? throw new InvalidOperationException($"Specified instance did not contain property associated with the specified binding {element.Binding}");
            int value = Convert.ToInt32(property.GetValue(concreteData));

            return BuildRenderTree(value, element);
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
            EnsureReferences();

            var property = concreteData!.GetType().GetProperties().SingleOrDefault(p => p.Name == element.Binding) ?? throw new InvalidOperationException($"Specified instance did not contain property associated with the specified binding {element.Binding}");
            bool value = Convert.ToBoolean(property.GetValue(concreteData));

            return BuildRenderTree(value, element);
        }

        public RenderFragment Visit(ArrayElementType<PersonalDetails> element)
        {
            EnsureReferences();

            var property = concreteData!.GetType().GetProperties().SingleOrDefault(p => p.Name == element.Binding) ?? throw new InvalidOperationException($"Specified instance did not contain property associated with the specified binding {element.Binding}");

            if (!property.PropertyType.IsGenericType || property.PropertyType.IsAssignableTo(typeof(IEnumerable<>)))
            {
                throw new InvalidOperationException("Array element type requires to be bound to a property of type implementing IEnumerable<T>");
            }

            var value = property.GetValue(concreteData) as IEnumerable ?? throw new InvalidOperationException("Value could not be retrieved from property in visit array element type");

            return BuildRenderTree(value!, element);
        }

        public RenderFragment Visit(ConditionalElementType<PersonalDetails> element)
        {
            EnsureReferences();

            var constraintProperty = concreteData!.GetType().GetProperties().SingleOrDefault(p => p.Name == element.ConstraintPropertyName) ?? throw new InvalidOperationException($"Specified instance did not contain property associated with the specified binding {element.ConstraintPropertyName}");
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
            EnsureReferences();

            var containerProperty = concreteData!.GetType().GetProperties().SingleOrDefault(p => p.Name == element.Binding) ?? throw new InvalidOperationException($"Specified instance did not contain property associated with the specified binding {element.Binding}");
            var containerInstance = containerProperty.GetValue(concreteData);
            var containerWidget = GetMostAppropriateWidget(element);
            var contentList = new List<RenderFragment>();

            return new RenderFragment(builder =>
            {
                builder.OpenComponent(1, typeof(ContainerWidget));

                foreach (var item in element.ContentElements)
                {
                    var elementProperty = containerInstance!.GetType().GetProperties().SingleOrDefault(p => p.Name == item.ElementType.Binding) ?? throw new InvalidOperationException($"Specified container type did not contain property associated with the specified binding. Path: {element.Binding}/{item.ElementType.Binding}");
                    var elementValue = elementProperty.GetValue(containerInstance);

                    var subFragment = BuildRenderTree(elementValue!, item.ElementType);
                    contentList.Add(subFragment);
                }

                builder.AddAttribute(100, "Value", contentList);
                builder.CloseComponent();
            });
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
                builder.AddMarkupContent(4, "<br/>");
            });
        }

        private void EnsureReferences()
        {
            if (concreteData == null)
            {
                throw new InvalidOperationException("Setting the concrete data is required before attempting to generate a render fragment");
            }

            if (deviceModel == null)
            {
                throw new InvalidOperationException("Setting the device model is required before attempting to generate a render fragment");
            }
        }
    }
}
