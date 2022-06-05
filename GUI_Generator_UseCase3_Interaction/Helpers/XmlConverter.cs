using System.Xml.Linq;
using System.Xml;
using GeneratorSharedComponents.Abstractions;
using GeneratorSharedComponents;
using Models.UseCases.IncludingUserInteraction.UseCase3;

namespace GUI_Generator_UseCase3_Interaction.Helpers
{
    public class XmlConverter : IXMLSpecificationConverter<EditToolBox>
    {
        /// <summary>
        /// Transforms an interface specification from XML to C# objects.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IEnumerable<InterfaceSpecificationElement<EditToolBox>> TransformToElementCollection(XElement root)
        {
            var xmlElements = root.Elements().Where(e => e.Parent == root);
            var interfaceElementCollection = new List<InterfaceSpecificationElement<EditToolBox>>();

            foreach (var item in xmlElements)
            {
                if (item.Attributes().Any(a => a.Name.LocalName.ToLower() == "xmlignore" && a.Value.ToLower() == "true"))
                {
                    continue;
                }

                interfaceElementCollection.Add(TransformXmlNodeToElement(item, root));
            }

            return interfaceElementCollection;
        }

      private InterfaceSpecificationElement<EditToolBox> TransformXmlNodeToElement(XElement node, XElement root)
        {
            var type = node.Attributes().FirstOrDefault(a => a.Name.LocalName.ToLower() == "type")?.Value ?? throw new XmlException($"Could not parse type attribute of node {node}");

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
                case "action":
                    return ParseActionType(node, root);
                default:
                    throw new XmlException("Node type not recognized");
            }
        }

        private InterfaceSpecificationElement<EditToolBox> ParseActionType(XElement element, XElement root)
        {
            var attributes = element.Attributes();

            var label = attributes.SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;
            var typeRef = attributes.SingleOrDefault(a => a.Name.LocalName.ToLower() == "actiontyperef")?.Value ?? throw new ArgumentException(nameof(element), "Action element requires an action type ref to another element");

            var referencedElement = root.Descendants().SingleOrDefault(e => e.Attributes().Any(a => a.Name.LocalName.ToLower() == "name" && a.Value == typeRef)) ?? throw new ArgumentException(nameof(root), "Root element did not contain element that action reference was pointing to");

            var actionContentElement = TransformXmlNodeToElement(referencedElement, root);

            return new InterfaceSpecificationElement<EditToolBox>(new ActionElementType<EditToolBox>(actionContentElement.ElementType, string.Empty, label));
        }

        private InterfaceSpecificationElement<EditToolBox> ParseFloatType(XElement element)
        {
            var bindingPath = element.Attributes().Single(a => a.Name == "Binding")?.Value ?? throw new ArgumentException(nameof(element), "Element missing Binding attribute");
            var label = element.Attributes().SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;

            return new InterfaceSpecificationElement<EditToolBox>(new FloatElementType<EditToolBox>(bindingPath, label));
        }

        private InterfaceSpecificationElement<EditToolBox> ParseIntegerType(XElement element)
        {
            var bindingPath = element.Attributes().Single(a => a.Name == "Binding")?.Value ?? throw new ArgumentException(nameof(element), "Element missing Binding attribute");
            var label = element.Attributes().SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;

            return new InterfaceSpecificationElement<EditToolBox>(new integerelementType<EditToolBox>(bindingPath, label));
        }

        private InterfaceSpecificationElement<EditToolBox> ParseConditionalType(XElement element, XElement root)
        {
            var attributes = element.Attributes();

            var subType = attributes.Single(a => a.Name.LocalName.ToLower() == "subtype")?.Value ?? throw new ArgumentException(nameof(element), "Element was supposed to be of type conditional but required attributes were not found");
            var condition = attributes.Single(a => a.Name.LocalName.ToLower() == "condition")?.Value ?? throw new ArgumentException(nameof(element), "Element was supposed to be of type conditional but required attributes were not found");
            var binding = attributes.Single(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? throw new ArgumentException(nameof(element), "Binding attribute was not specified in conditional type");
            var label = attributes.SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;
           
            var subElementType = this.GetSubElementType(subType, binding, label);
            XElement conditionElement = root.Descendants().FirstOrDefault(p => p.Name.LocalName == condition) ?? throw new ArgumentException(nameof(condition), $"Conditional element {condition} not found in descendants of root node");
            var conditionBindingPath = conditionElement.Attributes().Single(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? throw new ArgumentException(nameof(element), "Conditional element reference found, but no conditional binding specified");

            return new InterfaceSpecificationElement<EditToolBox>(new ConditionalElementType<EditToolBox>(subElementType, conditionBindingPath, label));
        }

        private InterfaceSpecificationElement<EditToolBox> ParseContainerType(XElement element, XElement root)
        {
            var attributes = element.Attributes();

            var binding = attributes.SingleOrDefault(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? string.Empty;
            var label = attributes.SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;
            var containerContentElements = ParseContainerContents(element, root);

            return new InterfaceSpecificationElement<EditToolBox>(new ContainerElementType<EditToolBox>(containerContentElements, binding, label));

        }

        private InterfaceSpecificationElement<EditToolBox> ParseArrayType(XElement element, XElement root)
        {
            var attributes = element.Attributes();

            var binding = attributes.Single(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? throw new ArgumentException(nameof(element), "Binding attribute was not specified in conditional type");
            var label = attributes.SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;
            var containerContentElements = ParseContainerContents(element, root);

            return new InterfaceSpecificationElement<EditToolBox>(new ArrayElementType<EditToolBox>(binding, label));
        }

        private InterfaceSpecificationElement<EditToolBox> ParseBoolType(XElement element)
        {
            var binding = element.Attributes().Single(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? throw new ArgumentException(nameof(element), "Binding attribute was not specified in conditional type");
            var label = element.Attributes().SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;

            return new InterfaceSpecificationElement<EditToolBox>(new BooleanElementType<EditToolBox>(binding, label));
        }

        private InterfaceSpecificationElement<EditToolBox> ParseStringType(XElement element)
        {
            var binding = element.Attributes().Single(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? throw new ArgumentException(nameof(element), "Binding attribute was not specified in conditional type");
            var label = element.Attributes().SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;

            return new InterfaceSpecificationElement<EditToolBox>(new StringElementType<EditToolBox>(binding, label));
        }

        private InterfaceElementType<EditToolBox> GetSubElementType(string subType, string binding, string? label)
        {
            switch (subType)
            {
                case "float":
                    return new FloatElementType<EditToolBox>(binding, label);
                case "int":
                    return new integerelementType<EditToolBox>(binding, label);
                default:
                    throw new ArgumentException(nameof(subType), "Sub type not recognized");
            }
        }

        private IEnumerable<InterfaceSpecificationElement<EditToolBox>> ParseContainerContents(XElement containerElement, XElement root)
        {
            List<InterfaceSpecificationElement<EditToolBox>> result = new List<InterfaceSpecificationElement<EditToolBox>>();

            foreach (var item in containerElement.Descendants())
            {
                result.Add(TransformXmlNodeToElement(item, root));
            }

            return result;
        }
    }
}
