using System.Xml.Linq;
using System.Xml;
using GeneratorSharedComponents.Abstractions;
using Models.UseCases.IncludingUserInteraction.UseCase1;
using GeneratorSharedComponents;

namespace GUI_Generator_UseCase1_InteractionHelpers
{
    public class XmlConverter : IXMLSpecificationConverter<LoginModel>
    {
        /// <summary>
        /// Transforms an interface specification from XML to C# objects.
        /// </summary>
        /// <param name="root"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public IEnumerable<InterfaceSpecificationElement<LoginModel>> TransformToElementCollection(XElement root)
        {
            var xmlElements = root.Elements().Where(e => e.Parent == root);
            var interfaceElementCollection = new List<InterfaceSpecificationElement<LoginModel>>();

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

        private InterfaceSpecificationElement<LoginModel> TransformXmlNodeToElement(XElement node, XElement root, bool bindingRequired = true)
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
                    return ParseContainerType(node, root, bindingRequired);
                case "array":
                    return ParseArrayType(node, root);
                case "action":
                    return ParseActionType(node, root);
                default:
                    throw new XmlException("Node type not recognized");
            }
        }

        private InterfaceSpecificationElement<LoginModel> ParseFloatType(XElement element)
        {
            var bindingPath = element.Attributes().Single(a => a.Name == "Binding")?.Value ?? throw new ArgumentException(nameof(element), "Element missing Binding attribute");
            var label = element.Attributes().SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;

            return new InterfaceSpecificationElement<LoginModel>(new FloatElementType<LoginModel>(bindingPath, label));
        }

        private InterfaceSpecificationElement<LoginModel> ParseIntegerType(XElement element)
        {
            var bindingPath = element.Attributes().Single(a => a.Name == "Binding")?.Value ?? throw new ArgumentException(nameof(element), "Element missing Binding attribute");
            var label = element.Attributes().SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;

            return new InterfaceSpecificationElement<LoginModel>(new integerelementType<LoginModel>(bindingPath, label));
        }

        private InterfaceSpecificationElement<LoginModel> ParseConditionalType(XElement element, XElement root)
        {
            var attributes = element.Attributes();

            var subType = attributes.Single(a => a.Name.LocalName.ToLower() == "subtype")?.Value ?? throw new ArgumentException(nameof(element), "Element was supposed to be of type conditional but required attributes were not found");
            var condition = attributes.Single(a => a.Name.LocalName.ToLower() == "condition")?.Value ?? throw new ArgumentException(nameof(element), "Element was supposed to be of type conditional but required attributes were not found");
            var binding = attributes.Single(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? throw new ArgumentException(nameof(element), "Binding attribute was not specified in conditional type");
            var label = attributes.SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;
           
            var subElementType = this.GetSubElementType(subType, binding, label);
            XElement conditionElement = root.Descendants().FirstOrDefault(p => p.Name.LocalName == condition) ?? throw new ArgumentException(nameof(condition), $"Conditional element {condition} not found in descendants of root node");
            var conditionBindingPath = conditionElement.Attributes().Single(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? throw new ArgumentException(nameof(element), "Conditional element reference found, but no conditional binding specified");

            return new InterfaceSpecificationElement<LoginModel>(new ConditionalElementType<LoginModel>(subElementType, conditionBindingPath, label));
        }

        private InterfaceSpecificationElement<LoginModel> ParseContainerType(XElement element, XElement root, bool bindingRequired = true)
        {
            var attributes = element.Attributes();

            var binding = attributes.SingleOrDefault(a => a.Name.LocalName.ToLower() == "binding")?.Value;

            if (binding == null && bindingRequired)
            {
                throw new ArgumentException(nameof(element), "Binding attribute was not specified in conditional type");
            }

            var label = attributes.SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;
            var containerContentElements = ParseContainerContents(element, root);

            return new InterfaceSpecificationElement<LoginModel>(new ContainerElementType<LoginModel>(containerContentElements, binding ?? string.Empty, label));
        }

        private InterfaceSpecificationElement<LoginModel> ParseArrayType(XElement element, XElement root)
        {
            var attributes = element.Attributes();

            var binding = attributes.Single(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? throw new ArgumentException(nameof(element), "Binding attribute was not specified in conditional type");
            var label = attributes.SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;
            var containerContentElements = ParseContainerContents(element, root);

            return new InterfaceSpecificationElement<LoginModel>(new ArrayElementType<LoginModel>(binding, label));
        }

        private InterfaceSpecificationElement<LoginModel> ParseActionType(XElement element, XElement root)
        {
            var attributes = element.Attributes();

            var label = attributes.SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;
            var typeRef = attributes.SingleOrDefault(a => a.Name.LocalName.ToLower() == "actiontyperef")?.Value ?? throw new ArgumentException(nameof(element), "Action element requires an action type ref to another element");

            var referencedElement = root.Descendants().SingleOrDefault(e => e.Attributes().Any(a => a.Name.LocalName.ToLower() == "name" && a.Value == typeRef)) ?? throw new ArgumentException(nameof(root), "Root element did not contain element that action reference was pointing to");

            var actionContentElement = TransformXmlNodeToElement(referencedElement, root, false);

            return new InterfaceSpecificationElement<LoginModel>(new ActionElementType<LoginModel>(actionContentElement.ElementType, string.Empty, label));
        }

        private InterfaceSpecificationElement<LoginModel> ParseBoolType(XElement element)
        {
            var binding = element.Attributes().Single(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? throw new ArgumentException(nameof(element), "Binding attribute was not specified in conditional type");
            var label = element.Attributes().SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;

            return new InterfaceSpecificationElement<LoginModel>(new BooleanElementType<LoginModel>(binding, label));
        }

        private InterfaceSpecificationElement<LoginModel> ParseStringType(XElement element)
        {
            var binding = element.Attributes().Single(a => a.Name.LocalName.ToLower() == "binding")?.Value ?? throw new ArgumentException(nameof(element), "Binding attribute was not specified in conditional type");
            var label = element.Attributes().SingleOrDefault(a => a.Name.LocalName.ToLower() == "label")?.Value;

            return new InterfaceSpecificationElement<LoginModel>(new StringElementType<LoginModel>(binding, label));
        }

        private InterfaceElementType<LoginModel> GetSubElementType(string subType, string binding, string? label)
        {
            switch (subType)
            {
                case "float":
                    return new FloatElementType<LoginModel>(binding, label);
                case "int":
                    return new integerelementType<LoginModel>(binding, label);
                default:
                    throw new ArgumentException(nameof(subType), "Sub type not recognized");
            }
        }

        private IEnumerable<InterfaceSpecificationElement<LoginModel>> ParseContainerContents(XElement containerElement, XElement root)
        {
            List<InterfaceSpecificationElement<LoginModel>> result = new List<InterfaceSpecificationElement<LoginModel>>();

            foreach (var item in containerElement.Descendants())
            {
                result.Add(TransformXmlNodeToElement(item, root));
            }

            return result;
        }
    }
}
