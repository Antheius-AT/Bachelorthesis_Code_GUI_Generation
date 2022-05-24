using GUI_Generator_UseCase1_Display.IndividualParts;
using GUI_Generator_UseCase1_Display.IndividualParts.Constraints.DeviceConstraints;
using GUI_Generator_UseCase1_Display.Widgets;

namespace GUI_Generator_UseCase1_Display.GeneratorInputs
{
    /// <summary>
    /// Represent the device model as per Supple's specification.
    /// The N function for mapping user effort required in navigating between components is not implemented.
    /// </summary>
    public class DeviceModel
    {
        public DeviceModel(IEnumerable<WidgetBase> templates, IEnumerable<AbstractDeviceConstraint> deviceConstraints, Func<AbstractInterfaceElement, WidgetBase, int> appropriatenessMeasuringFunction)
        {
            this.templates = templates;
            DeviceConstraints = deviceConstraints;
            AppropriatenessMeasuringFunction = appropriatenessMeasuringFunction;
        }

        /// <summary>
        /// Represent a set of widgets available on a specific device.
        /// </summary>
        IEnumerable<WidgetBase> templates { get; set; }

        /// <summary>
        /// Represents a set of device constraints.
        /// </summary>
        IEnumerable<AbstractDeviceConstraint> DeviceConstraints { get; set; }

        /// <summary>
        /// Represent a function capable of measuring how appropriate a widget is to render a specific abstract
        /// interface element.
        /// The resulting integer is the appropriateness score, the higher it is, the more appropriate the widget is.
        /// </summary>
        public Func<AbstractInterfaceElement, WidgetBase, int> AppropriatenessMeasuringFunction { get; set; }
    }
}
