using Microsoft.AspNetCore.Components;

namespace Attribute_Reflection_UseCase3_UserInteraction_Templates.Templates
{
    public abstract class TemplateBase : ComponentBase
    {
        [Parameter]
        public string? Label { get; set; }
    }
}
