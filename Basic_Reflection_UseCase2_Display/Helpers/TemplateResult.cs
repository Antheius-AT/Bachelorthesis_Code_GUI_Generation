namespace Basic_Reflection_UseCase2_Display_Template.Helpers
{
    public class TemplateResult
    {
        public TemplateResult(Type templateType, object? value, string label = "")
        {
            TemplateType = templateType;
            Value = value;
            Label = label;
        }

        public Type TemplateType { get; }
        public object? Value { get; }

        public string Label { get; }
    }
}
