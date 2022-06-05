using GeneratorSharedComponents;
using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;
using Models.UseCases.IncludingUserInteraction.UseCase3;

namespace GUI_Generator_UseCase3_Interaction.Generator
{
    public class AdaptiveInterfaceGenerator : IAdaptiveInterfaceGenerator<EditToolBox>
    {
        private readonly ISpecificationElementVisitor<EditToolBox> visitor;

        public AdaptiveInterfaceGenerator(ISpecificationElementVisitor<EditToolBox> visitor)
        {
            this.visitor = visitor;
        }

        public RenderFragment GenerateGUI(InterfaceSpecification<EditToolBox> specification, DeviceModel<EditToolBox> deviceModel, UserModel userModel, EditToolBox sensorData)
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
