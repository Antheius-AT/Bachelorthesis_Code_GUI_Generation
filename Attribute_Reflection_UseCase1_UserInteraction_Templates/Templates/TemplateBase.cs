using Attribute_Reflection_UseCase1_UserInteraction_Templates.Helpers;
using Microsoft.AspNetCore.Components;

namespace Attribute_Reflection_UseCase1_UserInteraction_Templates.Templates
{
    public abstract class TemplateBase : ComponentBase
    {
        /// <summary>
        /// Event to signal that value changed. 
        /// Could be a string, however I choose object for it to be a little more realistic (as not too many UIs only consist of string values)
        /// </summary>
        public event EventHandler<ValueChangedEventArgs> ValueChanged;
        
        protected virtual void RaiseValueChanged(object changed, string propertyName)
        {
            ValueChanged?.Invoke(this, new ValueChangedEventArgs(changed, propertyName));
        }
    }
}
