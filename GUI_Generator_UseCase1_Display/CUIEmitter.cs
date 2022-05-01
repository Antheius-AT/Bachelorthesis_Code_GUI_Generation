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

        public RenderFragment Emit(string specification)
        {
            var specificationToArray = specification.Split('\n', StringSplitOptions.RemoveEmptyEntries);
            var components = new List<string>();

            foreach (var item in specificationToArray)
            {
                if (item.StartsWith("<element"))
                {
                    var entry = item.Split(new string[] { "<element", "/>" }, StringSplitOptions.RemoveEmptyEntries).First();
                    var component = MapEntry(entry);

                    components.Add(component);
                }
                else if (item.StartsWith("<container"))
                {
                    throw new NotImplementedException();
                }
                else
                {
                    throw new ArgumentException(nameof(specification), "Specification is not valid");
                }
            }


            return new RenderFragment(builder =>
            {
                for (int i = 0; i < components.Count; i++)
                {
                    var markup = components[i].Replace(',', '.');
                    Console.WriteLine(markup);
                    builder.AddMarkupContent(i, markup);
                }
            });
        }

        /// <summary>
        /// Maps an abstract definition to a concrete element and returns a html string that can be rendered by a render fragment.
        /// </summary>
        /// <param name="entry"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException"></exception>
        /// <exception cref="Exception"></exception>
        private string MapEntry(string entry)
        {
            // At this point entry contains the attributes of the element tag.
            var elementAttributes = entry.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            string? typeName = null;
            string? value = null;
            string? label = null;

            foreach (var item in elementAttributes)
            {
                if (item.StartsWith("type"))
                {
                    typeName = item.Split("\"", StringSplitOptions.RemoveEmptyEntries).Last();
                }
                else if (item.StartsWith("value"))
                {
                    value = item.Split("\"", StringSplitOptions.RemoveEmptyEntries).Last();
                }
                else if (item.StartsWith("label"))
                {
                    label = item.Split("\"", StringSplitOptions.RemoveEmptyEntries).Last();
                }
            }

            if (string.IsNullOrWhiteSpace(typeName))
            {
                throw new ArgumentException(nameof(entry), "Type information was missing in abstract UI specification for entry");
            }

            var type = Type.GetType(typeName) ?? throw new ArgumentException(nameof(entry), "Type information was missing in abstract UI specification for entry"); ;

            var control = this.componentMap[type];

            if (type == typeof(string) && control is FormComponent<string>)
            {
                return $"<input type=text disabled/>";
            }
            else if (type == typeof(double) && control is FormComponent<double>)
            {
                return $"<input type=number disabled @bind-Value={data.CurrentTemperature}/>";
            }
            else if (type == typeof(bool) && control is FormComponent<bool>)
            {
                return $"<input type=checkbox disabled @bind-Value={data.IsPoweredOn}>";
            }

            throw new Exception();
        }
    }
}
