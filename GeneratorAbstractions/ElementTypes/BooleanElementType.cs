using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class BooleanElementType<TModelType> : InterfaceElementType<TModelType> where TModelType : class
    {
        public BooleanElementType(string bindingPath, string? label) : base("bool", typeof(bool), label)
        {
            BindingPath = bindingPath;
        }

        public string BindingPath { get; }

        public override RenderFragment Accept(ISpecificationElementVisitor<TModelType> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
