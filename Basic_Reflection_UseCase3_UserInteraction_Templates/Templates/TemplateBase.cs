using System.Reflection;
using Basic_Reflection_UseCase3_UserInteraction_Templates.Helpers;
using Microsoft.AspNetCore.Components;

namespace Basic_Reflection_UseCase3_UserInteraction_Templates.Templates
{
    public abstract class TemplateBase : ComponentBase
    {
        [Parameter]
        public string? Label { get; set; }

        [Parameter]
        public EventCallback<ValueChangedArguments> ValueChanged { get; set; }

        [Parameter]
        public PropertyInfo? PropertyInfo { get; set; }

        [Parameter]
        public object? AccessorInstance { get; set; }
    }
}
