using Attribute_Reflection_UseCase3_UserInteraction_Templates.Templates;
using Models.UseCases.IncludingUserInteraction.UseCase3;

namespace Attribute_Reflection_UseCase3_UserInteraction_RenderFragment.Helpers
{
    public static class WellKnownStyles
    {
        static WellKnownStyles()
        {
            ImageEditorStyles = new Dictionary<Type, Type>()
            {
                {typeof(int), typeof(SliderComponent) },
                {typeof(Action), typeof(ActionTemplate) },
                {typeof(Tools), typeof(SelectEnumList) }
            };
        }

        public static Dictionary<Type, Type> ImageEditorStyles { get; set; }
    }
}
