using System.Reflection;

namespace Attribute_Reflection_UseCase2_UserInteraction_RenderFragment.Helpers
{
    /// <summary>
    /// Contains an object reference and a property info to allow for a
    /// relatively safe reading and writing values from and to a property using reflection.
    /// </summary>
    public class ActivatableContainer
    {
        public ActivatableContainer(object instance, PropertyInfo property)
        {
            Instance = instance ?? throw new ArgumentNullException(nameof(instance), "Object reference must not be null");
            Property = property ?? throw new ArgumentNullException(nameof(property), "Property refernce must not be null");

            if (!instance.GetType().GetProperties().Any(p => p.Name == property.Name))
            {
                throw new ArgumentException(nameof(instance), "The provided instance does not contain a property matching the property passed as argument");
            }
        }

        public object Instance { get; }
        public PropertyInfo Property { get; }
    }
}
