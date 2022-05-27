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
            return new InterfaceSpecificationElement<SensorData>(new FloatElementType<SensorData>());
        }

        private InterfaceSpecificationElement<SensorData> ParseConditionalType(XElement element)
        {
            var subType = element.Attributes().Single(a => a.Name.LocalName.ToLower() == "subtype")?.Value ?? throw new ArgumentException(nameof(element), "Element was supposed to be of type conditional but required attributes were not found");
            var condition = element.Attributes().Single(a => a.Name.LocalName.ToLower() == "condition")?.Value ?? throw new ArgumentException(nameof(element), "Element was supposed to be of type conditional but required attributes were not found");
            var subElementType = this.GetSubElementType(subType);

            return new InterfaceSpecificationElement<SensorData>(new ConditionalElementType<SensorData>(subElementType, true));
        }

        private InterfaceSpecificationElement<SensorData> ParseBoolType(XElement element)
        {
            return new InterfaceSpecificationElement<SensorData>(new BooleanElementType<SensorData>());
        }

        private InterfaceElementType<SensorData> GetSubElementType(string subType)
        {
            switch (subType)
            {
                case "float":
                    return new FloatElementType<SensorData>();
                case "int":
                    return new integerelementType<SensorData>();
                default:
                    throw new ArgumentException(nameof(subType), "Sub type not recognized");
            }
        }
    }
}
