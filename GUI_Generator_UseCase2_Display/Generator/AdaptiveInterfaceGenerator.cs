using GeneratorSharedComponents;
using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;
using Models.UseCases.DisplayOnly.UseCase2;

namespace GUI_Generator_UseCase1_Display.Generator
{
    public class AdaptiveInterfaceGenerator : IAdaptiveInterfaceGenerator<PersonalDetails>
    {
        private readonly ISpecificationElementVisitor<PersonalDetails> visitor;

        public AdaptiveInterfaceGenerator(ISpecificationElementVisitor<PersonalDetails> visitor)
        {
            this.visitor = visitor;
        }

        public RenderFragment GenerateGUI(InterfaceSpecification<PersonalDetails> specification, DeviceModel<PersonalDetails> deviceModel, UserModel userModel, PersonalDetails sensorData)
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
