namespace GUI_Generator_UseCase1_Display.IndividualParts.ElementTypes
{
    public class ArrayElementType : InterfaceElementType
    {
        public ArrayElementType(InterfaceElementType vectorType)
        {
            VectorType = vectorType;
        }

        public InterfaceElementType VectorType { get; }
    }
}
