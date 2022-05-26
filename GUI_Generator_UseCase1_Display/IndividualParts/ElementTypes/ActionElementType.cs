namespace GUI_Generator_UseCase1_Display.IndividualParts.ElementTypes
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
