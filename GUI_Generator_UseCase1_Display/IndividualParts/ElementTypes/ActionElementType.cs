namespace GUI_Generator_UseCase1_Display.IndividualParts.ElementTypes
{
    public class ActionElementType : InterfaceElementType
    {
        public ActionElementType(InterfaceElementType actionType)
        {
            ActionType = actionType;
        }

        public InterfaceElementType ActionType { get; }
    }
}
