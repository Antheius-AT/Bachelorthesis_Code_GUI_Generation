using GeneratorSharedComponents.Abstractions;
using GeneratorSharedComponents.MarkerTypes;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    /// <summary>
    /// Represents a derivative element type of a functional interface specification.
    /// A derivative type as defined by Supple is a type with additional constraints.
    /// </summary>
    public class DerivativeElementType<TModelType> : InterfaceElementType<TModelType> where TModelType : class
    {
        public DerivativeElementType(InterfaceElementType<TModelType> type, IEnumerable<ElementConstraint> constraints, string binding, string? label) : base("derivative", typeof(Derivative), binding, label)
        {
            Type = type;
            Constraints = constraints;
        }

        public InterfaceElementType<TModelType> Type { get; }

        public IEnumerable<ElementConstraint> Constraints { get; }

        public override RenderFragment Accept(ISpecificationElementVisitor<TModelType> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
