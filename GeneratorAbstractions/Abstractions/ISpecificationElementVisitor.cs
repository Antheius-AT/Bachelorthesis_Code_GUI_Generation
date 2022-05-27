using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents.Abstractions
{
    /// <summary>
    /// Visitor for interface specification elements.
    /// </summary>
    /// <typeparam name="THandledModelType">The type of the underlying concrete data that is required for proper rendering and binding
    /// of interface elements.</typeparam>
    public interface ISpecificationElementVisitor<THandledModelType> where THandledModelType : class
    {
        RenderFragment Visit(StringElementType<THandledModelType> element);
        RenderFragment Visit(FloatElementType<THandledModelType> element);
        RenderFragment Visit(integerelementType<THandledModelType> element);
        RenderFragment Visit(DerivativeElementType<THandledModelType> element);
        RenderFragment Visit(ActionElementType<THandledModelType> element);
        RenderFragment Visit(BooleanElementType<THandledModelType> element);
        RenderFragment Visit(ArrayElementType<THandledModelType> element);
        RenderFragment Visit(ConditionalElementType<THandledModelType> element);
        RenderFragment Visit(ContainerElementType<THandledModelType> element);

        /// <summary>
        /// Sets an instance of the concrete model for the specific use case.
        /// </summary>
        /// <param name="data"></param>
        void SetData(THandledModelType data);

        /// <summary>
        /// Sets the device model, containing additional constraints and available widgets.
        /// </summary>
        /// <param name="deviceModel"></param>
        void SetDeviceModel(DeviceModel<THandledModelType> deviceModel);
    }
}
