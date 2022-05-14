namespace GUI_Generator_UseCase1_Display
{
    public class AUIEmitter
    {
        private List<Type> primitiveTypes;

        public AUIEmitter()
        {
            this.primitiveTypes = new List<Type>()
            {
                typeof(string),
                typeof(int),
                typeof(float),
                typeof(double),
                typeof(bool),
            };
        }

        public string Emit(object @object, string specification = "")
        {
            var properties = @object.GetType().GetProperties();

            foreach (var item in properties)
            {
                // If item is primitive type directly translate it
                // If item is complex type build container and start process anew with property being the new object.

                if (primitiveTypes.Contains(item.PropertyType))
                {
                    // This could be properly done by either using attributes or by clean code guidelines (e.g enforcing public setter for writeable properties only, to allow for dynamic evaluation of whether a property is read only)
                    if (item.PropertyType == typeof(bool))
                    {
                        specification += $"<element type=\"{item.PropertyType}\" value=\"{{{item.Name}}}\" label=\"{item.Name}\"/>\n";
                    }
                    else
                    {
                        specification += $"<element type=\"{item.PropertyType}\" flags=\"readonly\" value=\"{{{item.Name}}}\" label=\"{item.Name}\"/>\n";
                    }
                }
                else
                {
                    specification += "<container>";
                    var containerContent = this.Emit(item, specification);
                    specification += $"{containerContent}</container>";
                }
            }

            return specification;
        }
    }
}
