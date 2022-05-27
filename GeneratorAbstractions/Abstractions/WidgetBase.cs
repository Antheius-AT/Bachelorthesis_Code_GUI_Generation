using Microsoft.AspNetCore.Components;

namespace GeneratorSharedComponents.Abstractions
{
    public abstract class WidgetBase : ComponentBase
    {
        [Parameter]
        public string? Label { get; set; }
    }
}
