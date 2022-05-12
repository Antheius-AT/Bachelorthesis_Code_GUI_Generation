using System.ComponentModel;
using System.Reflection.Metadata.Ecma335;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using Models.UseCases.DisplayOnly.UseCase1;
using Radzen;
using Radzen.Blazor;

namespace GUI_Generator_UseCase1_Display
{
    public class CUIEmitter
    {
        private Dictionary<Type, RadzenComponent> componentMap;
        private SensorData data;

        public CUIEmitter(SensorData data)
        {
            componentMap = new Dictionary<Type, RadzenComponent>()
            {
                {typeof(bool), new RadzenSwitch() },
                {typeof(double), new RadzenNumeric<double>() },
                {typeof(int), new RadzenNumeric<int>() },
                {typeof(string), new RadzenTextBox() },
            };

            this.data = data;
        }

        public IEnumerable<(Type, string, string, string)> Emit(string specification)
        {
            var specificationToArray = specification.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var result = new List<(Type, string, string, string)>();

            foreach (var item in specificationToArray)
            {
                if (item.StartsWith("<element"))
                {
                    var entry = item.Split(new string[] { "<element", "/>" }, StringSplitOptions.RemoveEmptyEntries).First();
                    result.Add(TranslateEntry(entry));
                }
                else
                {
                    throw new ArgumentException(nameof(specification), "Specification is not valid for this use case");
                }
            }

            return result;
        }

        /// <summary>
        /// Translates a line of formal interface specification into a more concrete format, which can be further used to
        /// look for controls to render the data.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        private (Type, string, string, string) TranslateEntry(string entry)
        {
            // At this point entry contains the attributes of the element tag.
            var elementAttributes = entry.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string typeName = null!;
            string value = null!;
            string label = string.Empty;
            string flags = string.Empty;

            foreach (var item in elementAttributes)
            {
                if (item.StartsWith("type"))
                {
                    typeName = item.Split("\"", StringSplitOptions.RemoveEmptyEntries).Last() ?? throw new ArgumentException("Data type could not be parsed but must be specified");
                }
                else if (item.StartsWith("value"))
                {
                    value = item.Split("\"", StringSplitOptions.RemoveEmptyEntries).Last() ?? throw new ArgumentException("Entry value could not be parsed.");
                }
                else if (item.StartsWith("label"))
                {
                    label = item.Split("\"", StringSplitOptions.RemoveEmptyEntries).Last() ?? throw new ArgumentException("Label must not be empty if specified");
                }
                else if (item.StartsWith("flags"))
                {
                    flags = item.Split("\"", StringSplitOptions.RemoveEmptyEntries).Last() ?? throw new ArgumentException("Flags must not be empty if specified");
                }
            }

            var type = Type.GetType(typeName) ?? throw new ArgumentException(nameof(entry), "Type information was missing in abstract UI specification for entry"); ;
            
            return (type, value, label, flags);
        }
    }
}
