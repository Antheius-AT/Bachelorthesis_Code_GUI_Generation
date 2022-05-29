using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class StringElementType<TModelType> : InterfaceElementType<TModelType> where TModelType : class
    {
        public StringElementType(string binding, string? label) : base("string", typeof(string), binding, label)
        {
        }

        public override RenderFragment Accept(ISpecificationElementVisitor<TModelType> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
