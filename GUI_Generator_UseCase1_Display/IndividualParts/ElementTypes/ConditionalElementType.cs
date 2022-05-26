namespace GUI_Generator_UseCase1_Display.IndividualParts.ElementTypes
{
    public class ConditionalElementType : InterfaceElementType
    {
        public ConditionalElementType(InterfaceElementType elementType, string constraintSatisfied) : base("conditional")
        {
            ElementType = elementType;
            ConstraintSatisfied = constraintSatisfied;
        }

        public InterfaceElementType ElementType { get; }

        /// <summary>
        /// Rethink how to use this, wont stay a string.
        /// </summary>
        public string ConstraintSatisfied { get; }
    }
}
