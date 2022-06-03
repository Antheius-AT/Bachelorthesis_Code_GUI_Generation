using GeneratorSharedComponents;
using GeneratorSharedComponents.Abstractions;
using Models.Metadata;
using Models.UseCases.IncludingUserInteraction.UseCase2;

namespace GUI_Generator_UseCase2_Interaction.Helpers
{
    public static class HeuristicCalculator
    {
        public static int CalculateWidgetScore(InterfaceElementType<PersonalDetails> element, WidgetBase widget)
        {
            var customAttributes = widget.GetType().GetCustomAttributes(false);
            var valueKind = customAttributes.FirstOrDefault(a => a.GetType() == typeof(ValueKindAttribute)) as ValueKindAttribute ?? throw new ArgumentException(nameof(widget), "missing value kind attribute");
            var isEditable = customAttributes.Any(a => a.GetType() == typeof(EditableAttribute));

            if (valueKind.ValueKind != element.DotnetTypeRepresentation)
            {
                return -1;
            }

            if (isEditable)
            {
                return 10;
            }

            return 5;
        }
    }
}
