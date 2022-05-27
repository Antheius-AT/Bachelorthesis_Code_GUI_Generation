namespace GeneratorSharedComponents
{
    public class ActionElementType : InterfaceElementType
    {
        public ActionElementType(InterfaceElementType actionType) : base("action")
        {
            ActionType = actionType;
        }

        public InterfaceElementType ActionType { get; }
    }
}
