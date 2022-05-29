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
                interfaceElementCollection.Add(TransformXmlNodeToElement(item, root));
            }

            return interfaceElementCollection;
        }

        private InterfaceSpecificationElement<SensorData> TransformXmlNodeToElement(XElement node, XElement root)
        {
            var type = node.Attribute("Type")?.Value ?? throw new XmlException($"Could not parse type attribute of node {node}");

            switch (type)
            {
                case "float":
                    return ParseFloatType(node);
                case "conditional":
                    return ParseConditionalType(node, root);
                case "bool":
                    return ParseBoolType(node);
                default:
                    throw new XmlException("Node type not recognized");
            }
        }

        private InterfaceSpecificationElement<SensorData> ParseFloatType(XElement element)
        {
            var bindingPath = element.Attributes().Single(a => a.Name == "Binding")?.Value ?? throw new ArgumentException(nameof(element), "Element missing Binding attribute");
            var label = element.Attributes().SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;

            return new InterfaceSpecificationElement<SensorData>(new FloatElementType<SensorData>(bindingPath, label));
        }

        private InterfaceSpecificationElement<SensorData> ParseConditionalType(XElement element, XElement root)
        {
            var attributes = element.Attributes();

            var subType = attributes.Single(a => a.Name.LocalName.ToLower() == "subtype")?.Value ?? throw new ArgumentException(nameof(element), "Element was supposed to be of type conditional but required attributes were not found");
            var condition = attributes.Single(a => a.Name.LocalName.ToLower() == "condition")?.Value ?? throw new ArgumentException(nameof(element), "Element was supposed to be of type conditional but required attributes were not found");
            var binding = attributes.Single(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? throw new ArgumentException(nameof(element), "Binding attribute was not specified in conditional type");
            var label = attributes.SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;
           
            var subElementType = this.GetSubElementType(subType, binding, label);
            XElement conditionElement = root.Descendants().FirstOrDefault(p => p.Name.LocalName == condition) ?? throw new ArgumentException(nameof(condition), $"Conditional element {condition} not found in descendants of root node");
            var conditionBindingPath = conditionElement.Attributes().Single(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? throw new ArgumentException(nameof(element), "Conditional element reference found, but no conditional binding specified");

            return new InterfaceSpecificationElement<SensorData>(new ConditionalElementType<SensorData>(subElementType, conditionBindingPath, label));
        }

        private InterfaceSpecificationElement<SensorData> ParseBoolType(XElement element)
        {
            var binding = element.Attributes().Single(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? throw new ArgumentException(nameof(element), "Binding attribute was not specified in conditional type");
            var label = element.Attributes().SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;

            return new InterfaceSpecificationElement<SensorData>(new BooleanElementType<SensorData>(binding, label));
        }

        private InterfaceElementType<SensorData> GetSubElementType(string subType, string binding, string? label)
        {
            switch (subType)
            {
                case "float":
                    return new FloatElementType<SensorData>(binding, label);
                case "int":
                    return new integerelementType<SensorData>(label);
                default:
                    throw new ArgumentException(nameof(subType), "Sub type not recognized");
            }
        }
    }
}
