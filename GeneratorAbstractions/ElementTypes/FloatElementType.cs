using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class FloatElementType<TModelType> : InterfaceElementType<TModelType> where TModelType : class
    {
        public FloatElementType(string bindingPath) : base("float", typeof(float))
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
