using System.Xml.Linq;
using System.Xml;
using GeneratorSharedComponents.Abstractions;
using Models.UseCases.DisplayOnly.UseCase2;
using GeneratorSharedComponents;

namespace GUI_Generator_UseCase2_Display.Helpers
{
    public class XmlConverter : IXMLSpecificationConverter<PersonalDetails>
    {
        /// <summary>
        /// Transforms an interface specification from XML to C# objects.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IEnumerable<InterfaceSpecificationElement<PersonalDetails>> TransformToElementCollection(XElement root)
        {
            var xmlElements = root.Elements().Where(e => e.Parent == root);
            var interfaceElementCollection = new List<InterfaceSpecificationElement<PersonalDetails>>();

            foreach (var item in xmlElements)
            {
                interfaceElementCollection.Add(TransformXmlNodeToElement(item, root));
            }

            return interfaceElementCollection;
        }

        private InterfaceSpecificationElement<PersonalDetails> TransformXmlNodeToElement(XElement node, XElement root)
        {
            var type = node.Attribute("Type")?.Value ?? throw new XmlException($"Could not parse type attribute of node {node}");

            switch (type.ToLower())
            {
                case "float":
                    return ParseFloatType(node);
                case "conditional":
                    return ParseConditionalType(node, root);
                case "bool":
                    return ParseBoolType(node);
                case "string":
                    return ParseStringType(node);
                case "int":
                    return ParseIntegerType(node);
                case "container":
                    return ParseContainerType(node, root);
                case "array":
                    return ParseArrayType(node, root);
                default:
                    throw new XmlException("Node type not recognized");
            }
        }

        private InterfaceSpecificationElement<PersonalDetails> ParseFloatType(XElement element)
        {
            var bindingPath = element.Attributes().Single(a => a.Name == "Binding")?.Value ?? throw new ArgumentException(nameof(element), "Element missing Binding attribute");
            var label = element.Attributes().SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;

            return new InterfaceSpecificationElement<PersonalDetails>(new FloatElementType<PersonalDetails>(bindingPath, label));
        }

        private InterfaceSpecificationElement<PersonalDetails> ParseIntegerType(XElement element)
        {
            var bindingPath = element.Attributes().Single(a => a.Name == "Binding")?.Value ?? throw new ArgumentException(nameof(element), "Element missing Binding attribute");
            var label = element.Attributes().SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;

            return new InterfaceSpecificationElement<PersonalDetails>(new integerelementType<PersonalDetails>(bindingPath, label));
        }

        private InterfaceSpecificationElement<PersonalDetails> ParseConditionalType(XElement element, XElement root)
        {
            var attributes = element.Attributes();

            var subType = attributes.Single(a => a.Name.LocalName.ToLower() == "subtype")?.Value ?? throw new ArgumentException(nameof(element), "Element was supposed to be of type conditional but required attributes were not found");
            var condition = attributes.Single(a => a.Name.LocalName.ToLower() == "condition")?.Value ?? throw new ArgumentException(nameof(element), "Element was supposed to be of type conditional but required attributes were not found");
            var binding = attributes.Single(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? throw new ArgumentException(nameof(element), "Binding attribute was not specified in conditional type");
            var label = attributes.SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;
           
            var subElementType = this.GetSubElementType(subType, binding, label);
            XElement conditionElement = root.Descendants().FirstOrDefault(p => p.Name.LocalName == condition) ?? throw new ArgumentException(nameof(condition), $"Conditional element {condition} not found in descendants of root node");
            var conditionBindingPath = conditionElement.Attributes().Single(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? throw new ArgumentException(nameof(element), "Conditional element reference found, but no conditional binding specified");

            return new InterfaceSpecificationElement<PersonalDetails>(new ConditionalElementType<PersonalDetails>(subElementType, conditionBindingPath, label));
        }

        private InterfaceSpecificationElement<PersonalDetails> ParseContainerType(XElement element, XElement root)
        {
            var attributes = element.Attributes();

            var binding = attributes.Single(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? throw new ArgumentException(nameof(element), "Binding attribute was not specified in conditional type");
            var label = attributes.SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;
            var containerContentElements = ParseContainerContents(element, root);

            return new InterfaceSpecificationElement<PersonalDetails>(new ContainerElementType<PersonalDetails>(containerContentElements, binding, label));

        }

        private InterfaceSpecificationElement<PersonalDetails> ParseArrayType(XElement element, XElement root)
        {
            var attributes = element.Attributes();

            var binding = attributes.Single(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? throw new ArgumentException(nameof(element), "Binding attribute was not specified in conditional type");
            var label = attributes.SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;
            var containerContentElements = ParseContainerContents(element, root);

            return new InterfaceSpecificationElement<PersonalDetails>(new ArrayElementType<PersonalDetails>(binding, label));
        }

        private InterfaceSpecificationElement<PersonalDetails> ParseBoolType(XElement element)
        {
            var binding = element.Attributes().Single(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? throw new ArgumentException(nameof(element), "Binding attribute was not specified in conditional type");
            var label = element.Attributes().SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;

            return new InterfaceSpecificationElement<PersonalDetails>(new BooleanElementType<PersonalDetails>(binding, label));
        }

        private InterfaceSpecificationElement<PersonalDetails> ParseStringType(XElement element)
        {
            var binding = element.Attributes().Single(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? throw new ArgumentException(nameof(element), "Binding attribute was not specified in conditional type");
            var label = element.Attributes().SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;

            return new InterfaceSpecificationElement<PersonalDetails>(new StringElementType<PersonalDetails>(binding, label));
        }

        private InterfaceElementType<PersonalDetails> GetSubElementType(string subType, string binding, string? label)
        {
            switch (subType)
            {
                case "float":
                    return new FloatElementType<PersonalDetails>(binding, label);
                case "int":
                    return new integerelementType<PersonalDetails>(binding, label);
                default:
                    throw new ArgumentException(nameof(subType), "Sub type not recognized");
            }
        }

        private IEnumerable<InterfaceSpecificationElement<PersonalDetails>> ParseContainerContents(XElement containerElement, XElement root)
        {
            List<InterfaceSpecificationElement<PersonalDetails>> result = new List<InterfaceSpecificationElement<PersonalDetails>>();

            foreach (var item in containerElement.Descendants())
            {
                result.Add(TransformXmlNodeToElement(item, root));
            }

            return result;
        }
    }
}
