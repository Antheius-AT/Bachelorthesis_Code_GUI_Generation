using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents.Abstractions
{
    public interface IAdaptiveInterfaceGenerator
    {
        RenderFragment GenerateGUI(InterfaceSpecification specification, DeviceModel deviceModel, UserModel userModel);
    }
}
