
using GeneratorSharedComponents.Abstractions;

namespace GeneratorSharedComponents
{
    /// <summary>
    /// Represents a formal declarative interface specification.
    /// </summary>
    public class InterfaceSpecification<TModelType> where TModelType : class
    {
        public InterfaceSpecification(IEnumerable<InterfaceSpecificationElement<TModelType>> elements, IEnumerable<AbstractInterfaceConstraint> constraints)
        {
            InterfaceElements = elements;
            Constraints = constraints;
        }

        /// <summary>
        /// Represents the set of interface element as defined by the Supple paper.
        /// </summary>
        public IEnumerable<InterfaceSpecificationElement<TModelType>> InterfaceElements { get; set; }

        /// <summary>
        /// Represents the set of interface constraint as defined by the supple paper.
        /// </summary>
        public IEnumerable<AbstractInterfaceConstraint> Constraints { get; set; }
    }
}
