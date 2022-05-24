using GUI_Generator_UseCase1_Display.IndividualParts;

namespace GUI_Generator_UseCase1_Display.GeneratorInputs
{
    /// <summary>
    /// Represents a formal declarative interface specification.
    /// </summary>
    public class InterfaceSpecification
    {
        public InterfaceSpecification(IEnumerable<AbstractInterfaceElement> elements, IEnumerable<AbstractInterfaceConstraint> constraints)
        {
            Elements = elements;
            Constraints = constraints;
        }

        /// <summary>
        /// Represents the set of interface element as defined by the Supple paper.
        /// </summary>
        public IEnumerable<AbstractInterfaceElement> Elements { get; set; }

        /// <summary>
        /// Represents the set of interface constraint as defined by the supple paper.
        /// </summary>
        public IEnumerable<AbstractInterfaceConstraint> Constraints { get; set; }
    }
}
