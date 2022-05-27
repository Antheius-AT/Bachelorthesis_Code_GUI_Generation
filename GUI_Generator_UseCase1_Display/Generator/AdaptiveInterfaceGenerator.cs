using GeneratorSharedComponents;
using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;
using Models.UseCases.DisplayOnly.UseCase1;

namespace GUI_Generator_UseCase1_Display.Generator
{
    public class AdaptiveInterfaceGenerator : IAdaptiveInterfaceGenerator<SensorData>
    {
        private readonly ISpecificationElementVisitor<SensorData> visitor;

        public AdaptiveInterfaceGenerator(ISpecificationElementVisitor<SensorData> visitor)
        {
            this.visitor = visitor;
        }

        public RenderFragment GenerateGUI(InterfaceSpecification<SensorData> specification, DeviceModel<SensorData> deviceModel, UserModel userModel, SensorData sensorData)
        {
            if (!specification.InterfaceElements.Any())
            {
                throw new ArgumentException(nameof(specification), "Interface specification did not contain any elements");
            }
            else
            {
                visitor.SetData(sensorData);
                visitor.SetDeviceModel(deviceModel);

                return new RenderFragment(builder =>
                {
                    foreach (var item in specification.InterfaceElements)
                    {
                        var fragment = item.ElementType.Accept(visitor);
                        builder.AddContent(1, fragment);
                    }
                });
            }
        }
    }
}
