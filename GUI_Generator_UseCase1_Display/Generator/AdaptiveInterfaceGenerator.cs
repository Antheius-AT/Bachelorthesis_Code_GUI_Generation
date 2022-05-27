using GeneratorSharedComponents;
using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GUI_Generator_UseCase1_Display.Generator
{
    public class AdaptiveInterfaceGenerator : IAdaptiveInterfaceGenerator
    {
        private readonly ISpecificationElementVisitor visitor;

        public AdaptiveInterfaceGenerator(ISpecificationElementVisitor visitor)
        {
            this.visitor = visitor;
        }

        public RenderFragment GenerateGUI(InterfaceSpecification specification, DeviceModel deviceModel, UserModel userModel)
        {
            // For this demo there aren't any interface constraints. 
            // The variable was kept for the argument sake and
            // to stay as true to the original definition of Supple as possible.
            foreach (var item in specification.InterfaceElements)
            {
                item.ElementType.Accept(visitor);
            }

            throw new ArgumentException(nameof(specification), "Interface specification did not contain any elements");
        }
    }
}
