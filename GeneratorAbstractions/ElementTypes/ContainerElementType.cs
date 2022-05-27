namespace GeneratorSharedComponents
{
    public class ContainerElementType : InterfaceElementType
    {
        public ContainerElementType(IEnumerable<InterfaceElementType> contentElements) : base("container")
        {
            ContentElements = contentElements;
        }

        public IEnumerable<InterfaceElementType> ContentElements { get; }
    }
}
