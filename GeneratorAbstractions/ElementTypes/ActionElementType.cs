using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class ActionElementType<TModelType> : InterfaceElementType<TModelType> where TModelType : class
    {
        public ActionElementType(InterfaceElementType<TModelType> actionType) : base("action", typeof(Delegate))
        {
            ActionType = actionType;
        }

        public InterfaceElementType<TModelType> ActionType { get; }

        public override RenderFragment Accept(ISpecificationElementVisitor<TModelType> visitor)
        {
            return visitor.Visit(this);
        }
    }
}
