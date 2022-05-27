using GeneratorSharedComponents.Abstractions;

namespace GeneratorSharedComponents
{
    /// <summary>
    /// Represent the device model as per Supple's specification.
    /// The N function for mapping user effort required in navigating between components is not implemented.
    /// </summary>
    public class DeviceModel<TUnderlyingModelType> where TUnderlyingModelType : class
    {
        public DeviceModel(IEnumerable<WidgetBase> templates, IEnumerable<AbstractDeviceConstraint> deviceConstraints, Func<InterfaceElementType<TUnderlyingModelType>, WidgetBase, int> appropriatenessMeasuringFunction)
        {
            this.Templates = templates;
            DeviceConstraints = deviceConstraints;
            AppropriatenessMeasuringFunction = appropriatenessMeasuringFunction;
        }

        /// <summary>
        /// Represent a set of widgets available on a specific device.
        /// </summary>
        public IEnumerable<WidgetBase> Templates { get; set; }

        /// <summary>
        /// Represents a set of device constraints.
        /// </summary>
        public IEnumerable<AbstractDeviceConstraint> DeviceConstraints { get; set; }

        /// <summary>
        /// Represent a function capable of measuring how appropriate a widget is to render a specific abstract
        /// interface element.
        /// The resulting integer is the appropriateness score, the higher it is, the more appropriate the widget is.
        /// </summary>
        public Func<InterfaceElementType<TUnderlyingModelType>, WidgetBase, int> AppropriatenessMeasuringFunction { get; set; }
    }
}
