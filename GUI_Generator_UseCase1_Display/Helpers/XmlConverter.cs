using System.Xml.Linq;
using System.Xml;
using GeneratorSharedComponents.Abstractions;
using GeneratorSharedComponents;

namespace GUI_Generator_UseCase1_Display.Helpers
{
    public class XmlConverter
    {
        /// <summary>
        /// Transforms an interface specification from XML to C# objects.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IEnumerable<InterfaceSpecificationElement> TransformToElementCollection(XElement root)
        {
            var xmlElements = root.Elements().Where(e => e.Parent == root);
            var interfaceElementCollection = new List<InterfaceSpecificationElement>();

            foreach (var item in xmlElements)
            {
                interfaceElementCollection.Add(TransformXmlNodeToElement(item));
            }

            return interfaceElementCollection;
        }

        private InterfaceSpecificationElement TransformXmlNodeToElement(XElement node)
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

        private InterfaceSpecificationElement ParseFloatType(XElement element)
        {
            return new InterfaceSpecificationElement(new FloatElementType());
        }

        private InterfaceSpecificationElement ParseConditionalType(XElement element)
        {
            var subType = element.Attributes().Single(a => a.Name.LocalName.ToLower() == "subtype")?.Value ?? throw new ArgumentException(nameof(element), "Element was supposed to be of type conditional but required attributes were not found");
            var condition = element.Attributes().Single(a => a.Name.LocalName.ToLower() == "condition")?.Value ?? throw new ArgumentException(nameof(element), "Element was supposed to be of type conditional but required attributes were not found");
            var subElementType = this.GetSubElementType(subType);

            return new InterfaceSpecificationElement(new ConditionalElementType(subElementType, condition));
        }

        private InterfaceSpecificationElement ParseBoolType(XElement element)
        {
            return new InterfaceSpecificationElement(new BooleanElementType());
        }

        private InterfaceElementType GetSubElementType(string subType)
        {
            switch (subType)
            {
                case "float":
                    return new FloatElementType();
                case "int":
                    return new IntegerElementType();
                default:
                    throw new ArgumentException(nameof(subType), "Sub type not recognized");
            }
        }
    }
}
