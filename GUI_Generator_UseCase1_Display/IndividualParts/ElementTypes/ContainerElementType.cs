namespace GUI_Generator_UseCase1_Display.IndividualParts.ElementTypes
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
