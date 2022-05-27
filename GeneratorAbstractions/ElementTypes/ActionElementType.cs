using GeneratorSharedComponents.Abstractions;
using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents
{
    public class ActionElementType : InterfaceElementType
    {
        public ActionElementType(InterfaceElementType actionType) : base("action")
        {
            ActionType = actionType;
        }

        public InterfaceElementType ActionType { get; }

        public override RenderFragment Accept(ISpecificationElementVisitor visitor)
        {
            return visitor.Visit(this);
        }
    }
}
