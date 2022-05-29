using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class integerelementType<TModelType> : InterfaceElementType<TModelType> where TModelType : class
    {
        public integerelementType(string binding, string? label) : base("int", typeof(int), binding, label)
        {
        }

        public override RenderFragment Accept(ISpecificationElementVisitor<TModelType> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
