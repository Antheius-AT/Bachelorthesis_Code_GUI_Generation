namespace GUI_Generator_UseCase1_Display.IndividualParts.ElementTypes
{
    public abstract class InterfaceElementType
    {
        public InterfaceElementType(string stringRepresentation)
        {
            StringRepresentation = stringRepresentation;
        }

        public string StringRepresentation { get; }
    }
}
