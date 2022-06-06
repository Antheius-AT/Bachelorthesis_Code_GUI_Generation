using System.Reflection;

namespace Attribute_Reflection_UseCase2_UserInteraction_Templates.Helpers
{
    public class ValueChangedArguments
    {
        public ValueChangedArguments(object value, PropertyInfo sourceProperty, object accessorInstance)
        {
            Value = value;
            SourceProperty = sourceProperty;
            AccessorInstance = accessorInstance;
        }

        public object Value { get; }
        public PropertyInfo SourceProperty { get; }
        public object AccessorInstance { get; }
    }
}
