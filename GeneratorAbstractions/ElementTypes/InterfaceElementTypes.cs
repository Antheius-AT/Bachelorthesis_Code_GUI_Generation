namespace GeneratorSharedComponents
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
