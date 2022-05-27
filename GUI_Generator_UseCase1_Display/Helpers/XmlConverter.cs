using System.Xml.Linq;
using System.Xml;
using GeneratorSharedComponents.Abstractions;
using Models.UseCases.DisplayOnly.UseCase1;
using GeneratorSharedComponents;

namespace GUI_Generator_UseCase1_Display.Helpers
{
    public class XmlConverter : IXMLSpecificationConverter<SensorData>
    {
        /// <summary>
        /// Transforms an interface specification from XML to C# objects.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IEnumerable<InterfaceSpecificationElement<SensorData>> TransformToElementCollection(XElement root)
        {
            var xmlElements = root.Elements().Where(e => e.Parent == root);
            var interfaceElementCollection = new List<InterfaceSpecificationElement<SensorData>>();

            foreach (var item in xmlElements)
            {
                interfaceElementCollection.Add(TransformXmlNodeToElement(item));
            }

            return interfaceElementCollection;
        }

        private InterfaceSpecificationElement<SensorData> TransformXmlNodeToElement(XElement node)
        {
            var type = node.Attribute("Type")?.Value ?? throw new XmlException($"Could not parse type attribute of node {node}");

            switch (type)
            {
                case "float":
                    return ParseFloatType(node);
                case "conditional":
                    return ParseConditionalType(node);
                case "bool":
                    return ParseBoolType(node);
                default:
                    throw new XmlException("Node type not recognized");
            }
        }

        private InterfaceSpecificationElement<SensorData> ParseFloatType(XElement element)
        {
            var bindingPath = element.Attributes().Single(a => a.Name == "Binding")?.Value ?? throw new ArgumentException(nameof(element), "Element missing Binding attribute");
            return new InterfaceSpecificationElement<SensorData>(new FloatElementType<SensorData>(bindingPath));
        }

        private InterfaceSpecificationElement<SensorData> ParseConditionalType(XElement element)
        {
            var subType = element.Attributes().Single(a => a.Name.LocalName.ToLower() == "subtype")?.Value ?? throw new ArgumentException(nameof(element), "Element was supposed to be of type conditional but required attributes were not found");
            var condition = element.Attributes().Single(a => a.Name.LocalName.ToLower() == "condition")?.Value ?? throw new ArgumentException(nameof(element), "Element was supposed to be of type conditional but required attributes were not found");
            var binding = element.Attributes().Single(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? throw new ArgumentException(nameof(element), "Binding attribute was not specified in conditional type");
           
            var subElementType = this.GetSubElementType(subType, binding);

            return new InterfaceSpecificationElement<SensorData>(new ConditionalElementType<SensorData>(subElementType, true));
        }

        private InterfaceSpecificationElement<SensorData> ParseBoolType(XElement element)
        {
            var binding = element.Attributes().Single(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? throw new ArgumentException(nameof(element), "Binding attribute was not specified in conditional type");

            return new InterfaceSpecificationElement<SensorData>(new BooleanElementType<SensorData>(binding));
        }

        private InterfaceElementType<SensorData> GetSubElementType(string subType, string binding)
        {
            switch (subType)
            {
                case "float":
                    return new FloatElementType<SensorData>(binding);
                case "int":
                    return new integerelementType<SensorData>();
                default:
                    throw new ArgumentException(nameof(subType), "Sub type not recognized");
            }
        }
    }
}
