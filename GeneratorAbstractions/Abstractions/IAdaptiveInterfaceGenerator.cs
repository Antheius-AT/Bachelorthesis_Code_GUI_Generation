using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents.Abstractions
{
    public interface IAdaptiveInterfaceGenerator<TUnderlyingModelType> where TUnderlyingModelType : class
    {
        RenderFragment GenerateGUI(InterfaceSpecification<TUnderlyingModelType> specification, DeviceModel<TUnderlyingModelType> deviceModel, UserModel userModel, TUnderlyingModelType concreteData);
    }
}
