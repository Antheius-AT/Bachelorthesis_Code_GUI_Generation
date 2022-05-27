using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class BooleanElementType<TModelType> : InterfaceElementType<TModelType> where TModelType : class
    {
        public BooleanElementType(string bindingPath) : base("bool", typeof(bool))
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
