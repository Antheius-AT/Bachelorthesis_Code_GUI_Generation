namespace GeneratorSharedComponents
{
    public class ArrayElementType : InterfaceElementType
    {
        public ArrayElementType(InterfaceElementType vectorType) : base("array")
        {
            VectorType = vectorType;
        }

        public InterfaceElementType VectorType { get; }
    }
}
