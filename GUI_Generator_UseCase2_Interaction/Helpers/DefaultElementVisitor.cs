using System.Reflection;
using GeneratorSharedComponents;
using GeneratorSharedComponents.Abstractions;
using GUI_Generator_UseCase2_Interaction.Widgets;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Models.UseCases.IncludingUserInteraction.UseCase2;

namespace GUI_Generator_UseCase2_Interaction.Helpers
{
    public class DefaultElementVisitor : ISpecificationElementVisitor<PersonalDetails>
    {
        private PersonalDetails? concreteData;
        private DeviceModel<PersonalDetails>? deviceModel;
        private readonly IJSRuntime jsRuntime;

        public DefaultElementVisitor(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

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
            CheckReferences();

            var property = concreteData!.GetType().GetProperties().SingleOrDefault(p => p.Name == element.Binding) ?? throw new InvalidOperationException($"Specified instance did not contain property associated with the specified binding {element.Binding}");

            return BuildRenderTree(property, element);
        }

        public RenderFragment Visit(FloatElementType<PersonalDetails> element)
        {
            CheckReferences();

            var property = concreteData!.GetType().GetProperties().SingleOrDefault(p => p.Name == element.Binding) ?? throw new InvalidOperationException($"Specified instance did not contain property associated with the specified binding {element.Binding}");

            return BuildRenderTree(property, element);
        }

        public RenderFragment Visit(integerelementType<PersonalDetails> element)
        {
            CheckReferences();

            var property = concreteData!.GetType().GetProperties().SingleOrDefault(p => p.Name == element.Binding) ?? throw new InvalidOperationException($"Specified instance did not contain property associated with the specified binding {element.Binding}");

            return BuildRenderTree(property, element);
        }

        public RenderFragment Visit(DerivativeElementType<PersonalDetails> element)
        {
            throw new NotImplementedException();
        }

        public RenderFragment Visit(ActionElementType<PersonalDetails> element)
        {
            CheckReferences();

            var actionTypeFragment = element.ActionType?.Accept(this);

            return new RenderFragment(builder =>
            {
                builder.AddContent(10, actionTypeFragment);
                builder.OpenComponent(15, typeof(ActionButtonWidget));
                builder.AddAttribute(20, "Label", element.Label);
                builder.AddAttribute(30, "Action", EventCallback.Factory.Create(this, () => SaveCommand()));
                builder.CloseComponent();
            });
        }

        public RenderFragment Visit(BooleanElementType<PersonalDetails> element)
        {
            CheckReferences();

            var property = concreteData!.GetType().GetProperties().SingleOrDefault(p => p.Name == element.Binding) ?? throw new InvalidOperationException($"Specified instance did not contain property associated with the specified binding {element.Binding}");

            return BuildRenderTree(property, element);
        }

        public RenderFragment Visit(ArrayElementType<PersonalDetails> element)
        {
            CheckReferences();

            var property = concreteData!.GetType().GetProperties().SingleOrDefault(p => p.Name == element.Binding) ?? throw new InvalidOperationException($"Specified instance did not contain property associated with the specified binding {element.Binding}");

            if (!property.PropertyType.IsGenericType || property.PropertyType.IsAssignableTo(typeof(IEnumerable<>)))
            {
                throw new InvalidOperationException("Array element type requires to be bound to a property of type implementing IEnumerable<T>");
            }

            return BuildRenderTree(property!, element);
        }

        public RenderFragment Visit(ConditionalElementType<PersonalDetails> element)
        {
            CheckReferences();

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
            CheckReferences();

            if (!string.IsNullOrWhiteSpace(element.Binding))
            {
                return GetContainerWithBinding(element);
            }
            else
            {
                return GetContainerWithoutBinding(element);
            }
        }
        
        private RenderFragment GetContainerWithBinding(ContainerElementType<PersonalDetails> element)
        {
            var containerProperty = concreteData!.GetType().GetProperties().SingleOrDefault(p => p.Name == element.Binding) ?? throw new InvalidOperationException($"Specified instance did not contain property associated with the specified binding {element.Binding}");
            var containerInstance = containerProperty.GetValue(concreteData);
            var contentList = new List<RenderFragment>();

            return new RenderFragment(builder =>
            {
                builder.OpenComponent(1, typeof(ContainerWidget));

                foreach (var item in element.ContentElements)
                {
                    var elementProperty = containerInstance!.GetType().GetProperties().SingleOrDefault(p => p.Name == item.ElementType.Binding) ?? throw new InvalidOperationException($"Specified container type did not contain property associated with the specified binding. Path: {element.Binding}/{item.ElementType.Binding}");
                    var elementValue = elementProperty.GetValue(containerInstance);

                    var subFragment = BuildRenderTree(elementProperty!, item.ElementType, containerInstance);
                    contentList.Add(subFragment);
                }

                builder.AddAttribute(100, "Value", contentList);
                builder.CloseComponent();
            });
        }

        private RenderFragment GetContainerWithoutBinding(ContainerElementType<PersonalDetails> element)
        {
            var contentList = new List<RenderFragment>();

            return new RenderFragment(builder =>
            {
                builder.OpenComponent(10, typeof(ContainerWidget));

                foreach (var item in element.ContentElements)
                {
                    var subFragment = item.ElementType.Accept(this);
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

        private RenderFragment BuildRenderTree(PropertyInfo property, InterfaceElementType<PersonalDetails> element, object? accessorInstance = null)
        {
            var widget = this.GetMostAppropriateWidget(element);
            var value = property.GetValue(accessorInstance ?? concreteData);

            return new RenderFragment(builder =>
            {
                builder.OpenComponent(1, widget.GetType());
                builder.AddAttribute(2, "Value", value);
                builder.AddAttribute(3, "Label", element.Label);
                builder.AddAttribute(4, "ValueChanged", EventCallback.Factory.Create<ValueChangedArgs>(this, (val) => this.SetValue(property, accessorInstance ?? concreteData!, val)));
                builder.CloseComponent();
                builder.AddMarkupContent(6, "<br/>");
            });
        }

        private void CheckReferences()
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

        private void SetValue(PropertyInfo property, object accessorInstance, ValueChangedArgs args)
        {
            property.SetValue(accessorInstance, args.Value);
        }

        private void SaveCommand()
        {
            jsRuntime.InvokeVoidAsync("alert", $"Successfully saved:\nCurrent data: " +
                $"{concreteData!.FirstName}\n" +
                $" {concreteData.LastName}\n" +
                $"{concreteData.Gender}\n" +
                $"{concreteData.Age}\n" +
                $"{concreteData.Address.Street}\n" +
                $"{concreteData.Address.ZipCode} \n" +
                $"{concreteData.Address.City} \n" +
                $"{concreteData.Address.HouseNumber} \n");
        }
    }
}
