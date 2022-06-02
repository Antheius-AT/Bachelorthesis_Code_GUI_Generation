using GeneratorSharedComponents;
using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;
using Models.UseCases.IncludingUserInteraction.UseCase1;

namespace GUI_Generator_UseCase1_Interaction.Generator
{
    public class AdaptiveInterfaceGenerator : IAdaptiveInterfaceGenerator<LoginModel>
    {
        private readonly ISpecificationElementVisitor<LoginModel> visitor;

        public AdaptiveInterfaceGenerator(ISpecificationElementVisitor<LoginModel> visitor)
        {
            this.visitor = visitor;
        }

        public RenderFragment GenerateGUI(InterfaceSpecification<LoginModel> specification, DeviceModel<LoginModel> deviceModel, UserModel userModel, LoginModel loginModel)
        {
            if (!specification.InterfaceElements.Any())
            {
                throw new ArgumentException(nameof(specification), "Interface specification did not contain any elements");
            }
            else
            {
                visitor.SetData(loginModel);
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
