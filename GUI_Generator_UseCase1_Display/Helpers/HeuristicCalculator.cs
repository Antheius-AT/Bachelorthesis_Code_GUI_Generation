using GeneratorSharedComponents.Abstractions;
using GUI_Generator_UseCase1_Display.Widgets;
using Models.UseCases.DisplayOnly.UseCase1;

namespace GUI_Generator_UseCase1_Display.Helpers
{
    public static class HeuristicCalculator
    {
        public static int CalculateWidgetScore(InterfaceSpecificationElement<SensorData> element, WidgetBase widget)
        {
            return 5;
        }
    }
}
