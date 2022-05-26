using GUI_Generator_UseCase1_Display.IndividualParts;

namespace GUI_Generator_UseCase1_Display.GeneratorInputs
{
    /// <summary>
    /// Represents a formal declarative interface specification.
    /// </summary>
    public class InterfaceSpecification
    {
        public InterfaceSpecification(IEnumerable<InterfaceSpecificationElement> elements, IEnumerable<AbstractInterfaceConstraint> constraints)
        {
            InterfaceElements = elements;
            Constraints = constraints;
        }

        /// <summary>
        /// Represents the set of interface element as defined by the Supple paper.
        /// </summary>
        public IEnumerable<InterfaceSpecificationElement> InterfaceElements { get; set; }

        /// <summary>
        /// Represents the set of interface constraint as defined by the supple paper.
        /// </summary>
        public IEnumerable<AbstractInterfaceConstraint> Constraints { get; set; }
    }
}
