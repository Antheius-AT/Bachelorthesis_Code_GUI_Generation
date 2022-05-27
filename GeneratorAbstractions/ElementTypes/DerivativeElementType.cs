namespace GeneratorSharedComponents
{
    /// <summary>
    /// Represents a derivative element type of a functional interface specification.
    /// A derivative type as defined by Supple is a type with additional constraints.
    /// </summary>
    public class DerivativeElementType : InterfaceElementType
    {
        public DerivativeElementType(InterfaceElementType type, IEnumerable<ElementConstraint> constraints) : base("derivative")
        {
            Type = type;
            Constraints = constraints;
        }

        public InterfaceElementType Type { get; }

        public IEnumerable<ElementConstraint> Constraints { get; }
    }
}
